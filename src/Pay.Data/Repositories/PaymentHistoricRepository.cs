using Dapper;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Models;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class PaymentHistoricRepository : Repository, IPaymentHistoricRepository
    {
        public PaymentHistoricRepository(SqlServerOptions options)
            : base(options)
        {

        }

        public async Task InsertAsync(PaymentHistoric model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into PaymentHistorical (Id, PaymentId, Historic, CreatedAt) values (@Id, @PaymentId, @Historic, @CreatedAt)", new
                {
                    Id = model.Id,
                    PaymentId = model.Payment.Id,
                    Historic = model.Historic,
                    CreatedAt = model.CreatedAt
                });
            }
        }

        public async Task<PaymentHistoric> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<PaymentHistoric>("select * from PaymentHistorical where Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<PaymentHistoric>> SelectByPaymentIdAsync(Guid paymentId)
        {
            using (Connection)
            {
                return await Connection.QueryAsync<PaymentHistoric>("select * from PaymentHistorical where PaymentId = @PaymentId", new { PaymentId = paymentId });
            }
        }

        public async Task UpdateAsync(PaymentHistoric model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update PaymentHistorical set Historic = @Historic where Id = @Id", model);
            }
        }
    }
}