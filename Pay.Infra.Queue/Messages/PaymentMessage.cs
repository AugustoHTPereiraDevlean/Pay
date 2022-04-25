using Pay.Infra.Queue.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay.Infra.Queue.Messages
{
    public class PaymentMessage : MessageBase
    {
        public PaymentMessage(decimal price, decimal paidValue, decimal discount, Guid orderId)
            : base("payment")
        {
            Price = price;
            PaidValue = paidValue;
            Discount = discount;
            OrderId = orderId;
        }

        public decimal Price { get; set; }
        public decimal PaidValue { get; set; }
        public decimal Discount { get; set; }
        public Guid OrderId { get; set; }
    }
}
