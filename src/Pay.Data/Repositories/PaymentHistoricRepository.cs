using Dapper;
using Pay.Data.Abstractions;
using Pay.Data.Base;
using Pay.Data.Connection;
using Pay.Models;

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
                await Connection.ExecuteAsync("insert into PaymentHistoricals (Id, PaymentId, Historic, CreatedAt) values (@Id, @PaymentId, @Historic, @CreatedAt)", model);
            }
        }

        public async Task<PaymentHistoric> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<PaymentHistoric>("select * from PaymentHistoricals where Id = @Id", new { Id = id });
            }            
        }

        public async Task<IEnumerable<PaymentHistoric>> SelectByPaymentIdAsync(Guid paymentId)
        {
            using (Connection)
            {
                return await Connection.QueryAsync<PaymentHistoric>("select * from PaymentHistoricals where PaymentId = @PaymentId", new { PaymentId = paymentId });
            }
        }

        public async Task UpdateAsync(PaymentHistoric model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update PaymentHistoricals set Historic = @Historic where Id = @Id", model);
            }
        }
    }
}