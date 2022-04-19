using Pay.Data.Abstractions;
using Pay.Data.Base;
using Pay.Data.Connection;
using Pay.Models;

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

        public Task<OrderItem> SelectAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderItem model)
        {
            throw new NotImplementedException();
        }
    }
}