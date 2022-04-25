using Dapper;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Models;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class OrderRepository : Repository, IOrderRepository
    {
        public OrderRepository(SqlServerOptions options)
            : base(options)
        {

        }

        public async Task InsertAsync(Order model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into Orders (Id, UserId, PaymentMethod, CouponId, CreatedAt) values (@Id, @UserId, @PaymentMethod, @CouponId, @CreatedAt)", new
                {
                    Id = model.Id,
                    UserId = model.User.Id,
                    PaymentMethod = model.PaymentMethod.ToString(),
                    CreatedAt = model.CreatedAt,
                    CouponId = model.Coupon == null ? (dynamic)null : model.Coupon.Id,
                });
            }
        }

        public async Task<Order> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<Order>("select * from Orders where Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(Order model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update Orders set UserId = @UserId where Id = @Id", model);
            }
        }
    }
}