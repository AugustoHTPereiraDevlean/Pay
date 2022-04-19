using Pay.Data.Base;
using Pay.Models;

namespace Pay.Data.Abstractions
{
    public interface IPaymentHistoricRepository : IRepository<PaymentHistorical>
    {
        Task<IEnumerable<PaymentHistorical>> SelectByPaymentIdAsync(Guid paymentId);
    }
}