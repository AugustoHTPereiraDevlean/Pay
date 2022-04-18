using Pay.Models.Base;

namespace Pay.Models
{
    public class Coupon : Model
    {
        public Plan Plan { get; set; }
        public int? CountUses { get; set; }
        public bool IsActived { get; set; }
    }
}