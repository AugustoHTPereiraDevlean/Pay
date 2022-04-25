using Pay.Data.Base;
using Pay.Data.Connection;
using Dapper;
using Pay.Core.Models;
using Pay.Core.Abstractions.Repositories;

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
                await Connection.ExecuteAsync("insert into Subscriptions (Id, PlanId, UserId, OrderId, Price, IsActived, CreatedAt) values (@Id, @PlanId, @UserId, @OrderId, @Price, @IsActived, @CreatedAt)", new
                {
                    Id = model.Id,
                    PlanId = model.Plan.Id,
                    UserId = model.User.Id,
                    Price = model.Price,
                    IsActived = model.IsActived,
                    CreatedAt = model.CreatedAt,
                    OrderId = model.Order.Id,
                });
            }
        }

        public async Task<Subscription> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<Subscription>("select * from Subscriptions where Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<Subscription>> SelectByUserAsync(Guid userId)
        {
            using (Connection)
            {
                return await Connection.QueryAsync<Subscription, Plan, Subscription>(
                    sql: @" select S.*, P.* from Subscriptions S 
                            inner join Plans P on P.Id = S.PlanId
                            where S.UserId = 'a529e9c5-7565-4079-a1b5-a24bc9f67020'",
                    param: new { UserId = userId },
                    map: (subscription, plan) =>
                    {
                        subscription.Plan = plan;
                        return subscription;
                    }
                );
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