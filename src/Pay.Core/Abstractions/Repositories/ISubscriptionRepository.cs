using Pay.Core.Base;
using Pay.Core.Models;

namespace Pay.Core.Abstractions.Repositories
{
    public interface ISubscriptionRepository : IRepositoryBase<Subscription>
    {
        Task<IEnumerable<Subscription>> SelectByUserAsync(Guid userId);
    }
}