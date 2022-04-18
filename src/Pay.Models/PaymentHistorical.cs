using Pay.Models.Base;

namespace Pay.Models
{
    public class PaymentHistorical : Model
    {
        public Payment Payment { get; set; }
        public string Historic { get; set; }
    }
}