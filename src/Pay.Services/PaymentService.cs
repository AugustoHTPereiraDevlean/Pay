using Pay.Core.Abstractions.Repositories;
using Pay.Core.Abstractions.Services;
using Pay.Core.Base;
using Pay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay.Services
{
    public class PaymentService : ServiceBase, IPaymentService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentHistoricRepository _paymentHistoricRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionHistoricRepository _subscriptionHistoricRepository;

        public PaymentService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IPaymentHistoricRepository paymentHistoricRepository, ISubscriptionRepository subscriptionRepository)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _paymentHistoricRepository = paymentHistoricRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<ServiceResponse> CreatePayment(Guid orderId, decimal price, decimal discount, decimal paidValue)
        {
            try
            {
                var order = await _orderRepository.SelectAsync(orderId);

                var payment = new Payment();
                payment.Price = price;
                payment.Order = order;
                payment.Discount = discount;
                payment.PaidValue = paidValue;
                payment.Status = "Waiting";
                await _paymentRepository.InsertAsync(payment);

                var paymentHistoric = new PaymentHistoric();
                paymentHistoric.Payment = payment;
                paymentHistoric.Historic = "Created and waiting for the payment process";
                await _paymentHistoricRepository.InsertAsync(paymentHistoric);

                return CreateResponse(payment.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ServiceResponse> ProcessWaitingPayments(IEnumerable<Payment> payments)
        {
            #region Update payment status to processing
            foreach (var payment in payments)
            {
                payment.Status = "Processing";
                await _paymentRepository.UpdateAsync(payment);

                var paymentHistoric = new PaymentHistoric();
                paymentHistoric.Payment = payment;
                paymentHistoric.Historic = $"Start processing";
                await _paymentHistoricRepository.InsertAsync(paymentHistoric);
            }
            #endregion

            // Consume payment gateway

            foreach (var payment in payments)
            {
                #region Update payment status to paid
                payment.Status = "Paid";
                await _paymentRepository.UpdateAsync(payment);

                var paymentHistoric = new PaymentHistoric();
                paymentHistoric.Payment = payment;
                paymentHistoric.Historic = $"payment has been paid";
                await _paymentHistoricRepository.InsertAsync(paymentHistoric);
                #endregion

                #region Activate subscription
                var subscription = await _subscriptionRepository.SelectByOrderIdAsync(payment.Order.Id);
                if (subscription != null)
                {
                    if (!subscription.IsActived)
                    {
                        await _subscriptionRepository.UpdateIsActivedAsync(subscription.Id, true);

                        var subscriptionHistoric = new SubscriptionHistoric();
                        subscriptionHistoric.Subscription = subscription;
                        subscriptionHistoric.Historic = "subscription has been actived";
                        await _subscriptionHistoricRepository.InsertAsync(subscriptionHistoric);
                    }
                } 
                #endregion
            }

            return CreateResponse();
        }
    }
}
