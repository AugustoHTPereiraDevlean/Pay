using Pay.Core.Base;

namespace Pay.Core.Models
{
    public class SubscriptionHistoric : Model
    {
        public Subscription Subscription { get; set; }
        public string Historic { get; set; }
    }
}