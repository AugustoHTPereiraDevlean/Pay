using Dapper;
using Pay.Core.Models;
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
                await Connection.ExecuteAsync("insert into SubscriptionHistorical (Id, SubscriptionId, Historic, CreatedAt) values (@Id, @SubscriptionId, @Historic, @CreatedAt)", new
                {
                    Id = model.Id,
                    SubscriptionId = model.Subscription.Id,
                    Historic = model.Historic,
                    CreatedAt = model.CreatedAt
                });
            }
        }

        public async Task<SubscriptionHistoric> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<SubscriptionHistoric>("select * from SubscriptionHistorical where Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<SubscriptionHistoric>> SelectBySubscriptionIdAsync(Guid subscriptionId)
        {
            using (Connection)
            {
                return await Connection.QueryAsync<SubscriptionHistoric>("select * from SubscriptionHistorical where SubscriptionId = @SubscriptionId", new { SubscriptionId = subscriptionId });
            }
        }

        public Task UpdateAsync(SubscriptionHistoric model)
        {
            throw new NotImplementedException();
        }
    }
}