using Pay.Core.Abstractions.Repositories;
using Pay.Core.Abstractions.Services;
using Pay.Core.Base;
using Pay.Core.Models;
using Pay.Core.ValueObjects;

namespace Pay.Services
{
    public class SubscriptionService : ServiceBase, ISubscriptionService
    {
        private readonly IPlanRepository _planRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionHistoricRepository _subscriptionHistoricRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICouponPlanUserRepository _couponPlanUserRepository;

        public SubscriptionService(IUserRepository userRepository, ISubscriptionHistoricRepository subscriptionHistoricRepository, ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository, ICouponRepository couponRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, ICouponPlanUserRepository couponPlanUserRepository)
        {
            _userRepository = userRepository;
            _subscriptionHistoricRepository = subscriptionHistoricRepository;
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
            _couponRepository = couponRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _couponPlanUserRepository = couponPlanUserRepository;
        }

        public async Task<ServiceResponse> SubscribeAsync(Guid planId, Guid userId, PaymentMethod paymentMethod, string? discountCode = null)
        {
            // Check if plan exists
            var plan = await _planRepository.SelectAsync(planId);
            if (plan == null)
            {
                AddNotification("Plan not found");
            }

            // Check if user exists
            var user = await _userRepository.SelectAsync(userId);
            if (user == null)
            {
                AddNotification("User not found");
            }

            // Check if user already has subscribed to this plan
            var userSubscription = await _subscriptionRepository.SelectByUserAsync(userId);
            if (userSubscription != null)
            {
                if (userSubscription.Any(s => s.Plan.Id == planId && s.IsActived))
                {
                    AddNotification("User already subscribed to this plan");
                }
            }

            // Check if discount code exists and is valid for this context
            Coupon coupon = null;
            if (!string.IsNullOrWhiteSpace(discountCode))
            {
                if (!Guid.TryParse(discountCode, out Guid couponId))
                {
                    AddNotification("Invalid discount code");
                }

                coupon = await _couponRepository.SelectAsync(couponId);
                if (coupon == null)
                {
                    AddNotification("Invalid discount code");
                }
                else
                {
                    if (coupon.Plan.Id != planId)
                    {
                        AddNotification("Invalid discount code");
                    }

                    if (coupon.CountUses != 0)
                    {
                        var uses = await _couponPlanUserRepository.SelectUsesByUserAsync(userId, couponId);
                        if (uses.Count() >= coupon.CountUses)
                        {
                            AddNotification("Coupon uses limit reached");
                        }
                    }
                }
            }

            if (!IsSuccessfully)
            {
                return CreateResponse();
            }

            // Create order
            var orderItem = new OrderItem();
            orderItem.ObjectId = planId;
            orderItem.ObjectType = nameof(Plan);
            orderItem.Price = plan.Price;
            orderItem.Quantity = 1;

            var order = new Order();
            order.User = user;
            order.PaymentMethod = paymentMethod;
            order.Coupon = coupon;
            orderItem.Order = order;
            order.Items.Add(orderItem);

            // Create subscription
            var subscription = new Subscription();
            subscription.Plan = plan;
            subscription.User = user;
            subscription.Price = plan.Price;
            subscription.Order = order;
            subscription.IsActived = true;

            var subscriptionHistoric = new SubscriptionHistoric();
            subscriptionHistoric.Subscription = subscription;
            subscriptionHistoric.Historic = $"asigned to plan";

            // Store data
            await _orderRepository.InsertAsync(order);
            await _orderItemRepository.InsertAsync(orderItem);
            await _subscriptionRepository.InsertAsync(subscription);
            await _subscriptionHistoricRepository.InsertAsync(subscriptionHistoric);

            if (coupon != null)
                await _couponPlanUserRepository.InsertAsync(new CouponPlanUser { Coupon = coupon, Plan = plan, User = user });

            // TODO: Try to pay the subscription

            return CreateResponse(subscription.Id);
        }

        public Task UnSubscribeAsync(Guid subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}