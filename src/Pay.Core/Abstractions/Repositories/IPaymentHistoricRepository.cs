using Pay.Core.Base;
using Pay.Core.Models;

namespace Pay.Core.Abstractions.Repositories
{
    public interface IPaymentHistoricRepository : IRepositoryBase<PaymentHistoric>
    {
        Task<IEnumerable<PaymentHistoric>> SelectByPaymentIdAsync(Guid paymentId);
    }
}