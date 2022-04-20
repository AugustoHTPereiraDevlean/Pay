using Pay.Core.Base;

namespace Pay.Core.Models
{
    public class CouponPlanUser : Model
    {
        public Plan Plan { get; set; }
        public Coupon Coupon { get; set; }
        public User User { get; set; }
    }
}