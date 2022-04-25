using Newtonsoft.Json;
using Pay.Core.Abstractions.Queue;
using RabbitMQ.Client;
using System.Text;

namespace Pay.Infra.Queue
{
    public class QueueSender : ISender
    {
        private readonly ConnectionFactory _factory;

        public QueueSender(QueueOptions options)
        {
            _factory = new ConnectionFactory();
            _factory.Port = options.Port;
            _factory.HostName = options.Host;
            _factory.UserName = options.User;
            _factory.Password = options.Password;
        }

        public Task SendAsync(Message message)
        {
            if (string.IsNullOrEmpty(message.QueueName))
            {
                throw new ArgumentException(nameof(message.QueueName), "Must set message queue name.");
            }

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: message.QueueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false
                    );

                    byte[] encodedMessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: message.QueueName,
                        body: encodedMessage
                    );
                }
            }

            return Task.CompletedTask;
        }
    }
}
