using Pay.Core.Base;
using Pay.Core.Models;

namespace Pay.Core.Abstractions.Repositories
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        Task<IEnumerable<Payment>> SelectCreditCardWaitingPayment();
        Task<IEnumerable<Payment>> SelectBankslipWaitingPayment();
    }
}