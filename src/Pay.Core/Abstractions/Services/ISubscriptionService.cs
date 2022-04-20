using Pay.Core.ValueObjects;

namespace Pay.Core.Abstractions.Services
{
    public interface ISubscriptionService
    {
        Task<Guid> SubscribeAsync(Guid planId, Guid userId, PaymentMethod paymentMethod, string? discountCode = null);
        Task UnSubscribeAsync(Guid subscriptionId);
    }
}