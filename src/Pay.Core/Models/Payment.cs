using Pay.Core.Base;

namespace Pay.Core.Models
{
    public class Payment : Model
    {
        public Order Order { get; set; }
        public decimal Price { get; set; }
        public decimal PaidValue { get; set; }
        public decimal Discount { get; set; }
        public string Status { get; set; }
    }
}