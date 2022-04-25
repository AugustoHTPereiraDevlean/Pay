namespace Pay.Core.Base
{
    public class ServiceResponse
    {
        public Guid AggregateId { get; set; }
        public bool IsSuccessfully { get; set; }
        public string[] Notifications { get; set; }
    }
}