using Dapper;
using Pay.Data.Abstractions;
using Pay.Data.Base;
using Pay.Data.Connection;
using Pay.Models;

namespace Pay.Data.Repositories
{
    public class PlanRepository : Repository, IPlanRepository
    {
        public PlanRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(Plan model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into Plans (Id, Name, Description, Price, BillingInterval, Key, IsActived, CreatedAt) values (@Id, @Name, @Description, @Price, @BillingInterval, @Key, @IsActived, @CreatedAt)", model);
            }
        }

        public async Task<Plan> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<Plan>("select * from Plans where Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<Plan>> SelectAsync()
        {
            using (Connection)
            {
                return await Connection.QueryAsync<Plan>("select * from Plans where IsActived = 1");
            }
        }

        public async Task UpdateAsync(Plan model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update Plans set Name = @Name, Description = @Description, Price = @Price, BillingInterval = @BillingInterval, Key = @Key, IsActived = @IsActived where Id = @Id", model); 
            }
        }
    }
}