using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay.Core.Abstractions.Queue
{
    public abstract class Message
    {
        public Message(string queueName)
        {
            QueueName = queueName;
        }

        public string QueueName { get; }
    }
}
