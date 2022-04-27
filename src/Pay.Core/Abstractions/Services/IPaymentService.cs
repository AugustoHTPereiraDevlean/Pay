using Pay.Core.Base;
using Pay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay.Core.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<ServiceResponse> CreatePayment(Guid orderId, decimal price, decimal discount, decimal paidValue);
        Task<ServiceResponse> ProcessWaitingPayments(IEnumerable<Payment> payments);
    }
}
