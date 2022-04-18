using Pay.Models.Base;

namespace Pay.Models
{
    public class Order : Model
    {
        public User User { get; set; }
    }
}