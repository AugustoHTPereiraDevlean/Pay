using Pay.Core.Base;
using Pay.Core.ValueObjects;

namespace Pay.Core.Models
{
    public class Coupon : Model
    {
        public Plan Plan { get; set; }
        public int? CountUses { get; set; }
        public BenefitType BenefitType { get; set; }
        public decimal Benefit { get; set; }
        public bool IsActived { get; set; }
    }
}