using Pay.Core.Base;
using Pay.Core.Models;

namespace Pay.Core.Abstractions.Repositories
{
    public interface ISubscriptionHistoricRepository : IRepositoryBase<SubscriptionHistoric>
    {
        Task<IEnumerable<SubscriptionHistoric>> SelectBySubscriptionIdAsync(Guid subscriptionId);
    }
}