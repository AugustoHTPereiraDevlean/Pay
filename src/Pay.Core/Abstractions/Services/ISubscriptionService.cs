using Pay.Core.Base;
using Pay.Core.ValueObjects;

namespace Pay.Core.Abstractions.Services
{
    public interface ISubscriptionService
    {
        Task<ServiceResponse> SubscribeAsync(Guid planId, Guid userId, PaymentMethod paymentMethod, string? discountCode = null);
        Task UnSubscribeAsync(Guid subscriptionId);
    }
}