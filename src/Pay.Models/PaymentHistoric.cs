using Pay.Models.Base;

namespace Pay.Models
{
    public class PaymentHistoric : Model
    {
        public Payment Payment { get; set; }
        public string Historic { get; set; }
    }
}