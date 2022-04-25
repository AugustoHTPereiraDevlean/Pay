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
                await Connection.ExecuteAsync("insert into Coupons (Id, PlanId, CountUses, IsActived, CreatedAt) values (@Id, @PlanId, @CountUses, @IsActived, @CreatedAt)", new
                {
                    Id = model.Id,
                    PlanId = model.Plan.Id,
                    CountUses = model.CountUses,
                    IsActived = model.IsActived,
                    CreatedAt = model.CreatedAt
                });
            }
        }

        public async Task<Coupon> SelectAsync(Guid id)
        {
            using (Connection)
            {
                var coupons = await Connection.QueryAsync<Coupon, Plan, Coupon>(
                    sql: "select * from Coupons C inner join Plans P on P.Id = C.PlanId where C.Id = @Id",
                    param: new { Id = id },
                    map: (coupon, plan) =>
                    {
                        coupon.Plan = plan;
                        return coupon;
                    }
                );

                return coupons.FirstOrDefault();
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