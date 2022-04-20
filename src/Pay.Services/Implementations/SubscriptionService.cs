using Pay.Core.Abstractions.Services;
using Pay.Core.ValueObjects;

namespace Pay.Services.Implementations
{
    public class SubscriptionService : ISubscriptionService
    {

        public Task<Guid> SubscribeAsync(Guid planId, Guid userId, PaymentMethod paymentMethod, string? discountCode = null)
        {
            // TODO: Check if plan exists

            // TODO: Check if user exists

            // TODO: Check if user already has subscribed to this plan

            // TODO: Check if discount code exists and is valid for this context

            // TODO: Create order

            // TODO: Create subscription

            // TODO: Try to pay the subscription

            throw new NotImplementedException();
        }

        public Task UnSubscribeAsync(Guid subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}