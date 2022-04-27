using Pay.Core.Base;
using Pay.Core.Models;

namespace Pay.Core.Abstractions.Repositories
{
    public interface ISubscriptionRepository : IRepositoryBase<Subscription>
    {
        Task<IEnumerable<Subscription>> SelectByUserAsync(Guid userId);
        Task<Subscription> SelectByOrderIdAsync(Guid orderId);
        Task UpdateIsActivedAsync(Guid subscriptionId, bool isActived);
        Task<IEnumerable<Subscription>> SelectActivedAsync();
    }
}