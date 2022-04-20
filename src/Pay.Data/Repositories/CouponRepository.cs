using Dapper;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Models;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class CouponRepository : Repository, ICouponRepository
    {
        public CouponRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(Coupon model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into Coupons (Id, PlanId, CountUses, IsActived, CreatedAt) values (@Id, @PlanId, @CountUses, @IsActived, @CreatedAt)", model);
            }
        }

        public async Task<Coupon> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<Coupon>("select * from Coupons where Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(Coupon model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update Coupons set PlanId = @PlanId, CountUses = @CountUses, IsActived = @IsActived where Id = @Id", model); 
            }
        }
    }
}