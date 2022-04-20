using Pay.Core.Base;

namespace Pay.Core
{
    public class SubscriptionHistoric : Model
    {
        public Subscription Subscription { get; set; }
        public string Historic { get; set; }
    }
}