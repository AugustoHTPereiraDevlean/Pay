using Pay.Core.Base;

namespace Pay.Core.Models
{
    public class Plan : Model
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BillingInterval { get; set; }
        public string Key { get; set; }
        public bool IsActived { get; set; }
    }
}