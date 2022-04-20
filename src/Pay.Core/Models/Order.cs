using Pay.Core.Base;

namespace Pay.Core.Models
{
    public class Order : Model
    {
        public User User { get; set; }
        public IList<OrderItem> Items { get; set; }
    }
}