using Pay.Core.Base;

namespace Pay.Core.Models
{
    public class PaymentHistoric : Model
    {
        public Payment Payment { get; set; }
        public string Historic { get; set; }
    }
}