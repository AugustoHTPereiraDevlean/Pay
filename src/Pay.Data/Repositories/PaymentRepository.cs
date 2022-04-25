using Dapper;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Models;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class PaymentRepository : Repository, IPaymentRepository
    {
        public PaymentRepository(SqlServerOptions options)
            : base(options)
        {

        }

        public async Task InsertAsync(Payment model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into Payments (Id, OrderId, Price, PaidValue, Discount, Status, CreatedAt) values (@Id, @OrderId, @Price, @PaidValue, @Discount, @Status, @CreatedAt)", new
                {
                    Id = model.Id,
                    OrderId = model.Order.Id,
                    Price = model.Price,
                    Status = model.Status,
                    CreatedAt = model.CreatedAt,
                    PaidValue = model.PaidValue,
                    Discount = model.Discount
                });
            }
        }

        public async Task<Payment> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<Payment>("select * from Payments where Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(Payment model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update Payments set OrderId = @OrderId, Price = @Price, Status = @Status, CreatedAt = @CreatedAt where Id = @Id", model);
            }
        }
    }
}