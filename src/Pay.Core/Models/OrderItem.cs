using Pay.Core.Base;

namespace Pay.Core.Models
{
    public class OrderItem : Model
    {
        public Order Order { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}