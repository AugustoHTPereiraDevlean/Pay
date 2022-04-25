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

        public decimal TotalValue => Items.Sum(x => x.Quantity * x.Price);
        public decimal Discount
        {
            get
            {
                if (Coupon == null)
                    return 0;

                switch (Coupon.BenefitType)
                {   
                    case BenefitType.Percent:
                        return TotalValue * Coupon.Benefit / 100;
                    case BenefitType.Value:
                        return Coupon.Benefit;
                    default:
                        return 0;
                }
            }
        }
    }
}