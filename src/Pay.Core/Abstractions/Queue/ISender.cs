namespace Pay.Core.Abstractions.Queue
{
    public interface ISender
    {
        Task SendAsync(Message message);
    }
}
