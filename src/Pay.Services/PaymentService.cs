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

        public PaymentService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IPaymentHistoricRepository paymentHistoricRepository)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _paymentHistoricRepository = paymentHistoricRepository;
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
    }
}
