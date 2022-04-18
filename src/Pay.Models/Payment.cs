using Pay.Models.Base;

namespace Pay.Models
{
    public class Payment : Model
    {
        public Order Order { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}