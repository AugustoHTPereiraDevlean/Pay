using Pay.Core.ValueObjects;

namespace Pay.Presentation.WebAPI.Requests
{
    public class SubscribeRequest
    {
        public Guid PlanId { get; set; }
        public Guid UserId { get; set; }
        public Guid? CouponId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}