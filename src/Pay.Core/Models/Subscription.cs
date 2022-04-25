using Pay.Core.Base;
using Pay.Core.ValueObjects;

namespace Pay.Core.Models
{
    public class Subscription : Model
    {
        public Plan Plan { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }
        public decimal Price { get; set; }
        public bool IsActived { get; set; }
    }
}