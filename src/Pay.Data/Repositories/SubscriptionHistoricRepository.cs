using Dapper;
using Pay.Core;
using Pay.Core.Abstractions.Repositories;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class SubscriptionHistoricRepository : Repository, ISubscriptionHistoricRepository
    {
        public SubscriptionHistoricRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(SubscriptionHistoric model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into SubscriptionHistoricals (Id, SubscriptionId, Historic, CreatedAt) values (@Id, @SubscriptionId, @Historic, @CreatedAt)", model);
            }
        }

        public async Task<SubscriptionHistoric> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<SubscriptionHistoric>("select * from SubscriptionHistoricals where Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<SubscriptionHistoric>> SelectBySubscriptionIdAsync(Guid subscriptionId)
        {
            using (Connection)
            {
                return await Connection.QueryAsync<SubscriptionHistoric>("select * from SubscriptionHistoricals where SubscriptionId = @SubscriptionId", new { SubscriptionId = subscriptionId });
            }
        }

        public Task UpdateAsync(SubscriptionHistoric model)
        {
            throw new NotImplementedException();
        }
    }
}