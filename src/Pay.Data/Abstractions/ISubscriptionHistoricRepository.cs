using Pay.Data.Base;
using Pay.Models;

namespace Pay.Data.Abstractions
{
    public interface ISubscriptionHistoricRepository : IRepository<SubscriptionHistoric>
    {
        Task<IEnumerable<SubscriptionHistoric>> SelectBySubscriptionIdAsync(Guid subscriptionId);
    }
}