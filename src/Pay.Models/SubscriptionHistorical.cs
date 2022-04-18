using Pay.Models.Base;

namespace Pay.Models
{
    public class SubscriptionHistorical : Model
    {
        public Subscription Subscription { get; set; }
        public string Historic { get; set; }
    }
}