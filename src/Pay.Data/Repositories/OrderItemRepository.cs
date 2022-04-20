using Dapper;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Models;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class OrderItemRepository : Repository, IOrderItemRepository
    {
        public OrderItemRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(OrderItem model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into OrderItems (Id, OrderId, ObjectId, ObjectType, Price, Quantity, CreatedAt) values (@Id, @OrderId, @ObjectId, @ObjectType, @Price, @Quantity, @CreatedAt)", model);
            }
        }

        public async Task<OrderItem> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<OrderItem>("select * from OrderItems where Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(OrderItem model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update OrderItems set OrderId = @OrderId, ObjectId = @ObjectId, ObjectType = @ObjectType, Price = @Price, Quantity = @Quantity, CreatedAt = @CreatedAt where Id = @Id", model); 
            }
        }
    }
}