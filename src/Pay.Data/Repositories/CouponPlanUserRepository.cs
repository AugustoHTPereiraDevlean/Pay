using Dapper;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Models;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class CouponPlanUserRepository : Repository, ICouponPlanUserRepository
    {
        public CouponPlanUserRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(CouponPlanUser model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into CouponPlanUser (Id, PlanId, CouponId, UserId, CreatedAt) values (@Id, @PlanId, @CouponId, @UserId, @CreatedAt)", model);
            }
        }

        public async Task<CouponPlanUser> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<CouponPlanUser>("select * from CouponPlanUser where Id = @Id", new { Id = id });
            }
        }

        public Task<IEnumerable<CouponPlanUser>> SelectUsesByUserAsync(Guid userId, Guid couponId)
        {
            using (Connection)
            {
                return Connection.QueryAsync<CouponPlanUser>("select * from CouponPlanUser where UserId = @UserId and CouponId = @CouponId", new { UserId = userId, CouponId = couponId });
            }
        }

        public Task UpdateAsync(CouponPlanUser model)
        {
            throw new NotImplementedException();
        }
    }
}