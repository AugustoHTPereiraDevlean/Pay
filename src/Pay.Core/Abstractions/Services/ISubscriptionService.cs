using Pay.Core.ValueObjects;
using Pay.Services.Responses;

namespace Pay.Core.Abstractions.Services
{
    public interface ISubscriptionService
    {
        Task SubscribeAsync(Guid planId, Guid userId, PaymentMethod paymentMethod, string? discountCode = null);
        Task UnSubscribeAsync(Guid subscriptionId);
    }
}