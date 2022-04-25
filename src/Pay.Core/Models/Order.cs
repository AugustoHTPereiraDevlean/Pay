using Pay.Core.Base;
using Pay.Core.ValueObjects;

namespace Pay.Core.Models
{
    public class Order : Model
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public User User { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Coupon Coupon { get; set; }
        public IList<OrderItem> Items { get; set; }
    }
}