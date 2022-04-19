using Pay.Models.Base;

namespace Pay.Models
{
    public class SubscriptionHistoric : Model
    {
        public Subscription Subscription { get; set; }
        public string Historic { get; set; }
    }
}