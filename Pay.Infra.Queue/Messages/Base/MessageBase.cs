using Pay.Core.Abstractions.Queue;

namespace Pay.Infra.Queue.Messages.Base
{
    public abstract class MessageBase : Message
    {
        protected MessageBase(string queueName) 
            : base(queueName)
        {

        }
    }
}
