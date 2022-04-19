using Dapper;
using Pay.Data.Abstractions;
using Pay.Data.Base;
using Pay.Data.Connection;
using Pay.Models;

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
                await Connection.ExecuteAsync("insert into Orders (Id, UserId, CreatedAt) values (@Id, @UserId, @CreatedAt)", model);
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