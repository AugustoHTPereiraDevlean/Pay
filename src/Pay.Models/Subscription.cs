using Pay.Models.Base;

namespace Pay.Models
{
    public class Subscription : Model
    {
        public Plan Plan { get; set; }
        public User User { get; set; }
        public decimal Price { get; set; }
        public bool IsActived { get; set; }
    }
}