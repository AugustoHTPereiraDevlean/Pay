using Pay.Data.Abstractions;
using Pay.Data.Base;
using Pay.Data.Connection;
using Pay.Models;
using Dapper;

namespace Pay.Data.Repositories
{
    public class SubscriptionRepository : Repository, ISubscriptionRepository
    {
        public SubscriptionRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(Subscription model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into Subscriptions (Id, PlanId, UserId, Price, IsActived, CreatedAt) values (@Id, @PlanId, @UserId, @Price, @IsActived, @CreatedAt)", model);
            }
        }

        public async Task<Subscription> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<Subscription>("select * from Subscriptions where Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(Subscription model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update Subscriptions set PlanId = @PlanId, UserId = @UserId, Price = @Price, IsActived = @IsActived where Id = @Id", model); 
            }
        }
    }
}