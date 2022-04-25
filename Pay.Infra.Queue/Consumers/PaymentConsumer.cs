using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pay.Core.Abstractions.Services;
using Pay.Infra.Queue.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Pay.Infra.Queue.Workers
{
    public class PaymentConsumer : BackgroundService
    {
        public const string QUEUE_NAME = "payment";

        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly IServiceProvider _serviceProvider;

        public PaymentConsumer(QueueOptions options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory();
            factory.Port = options.Port;
            factory.HostName = options.Host;
            factory.UserName = options.User;
            factory.Password = options.Password;

            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(QUEUE_NAME, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += OnReceivePaymentMessage;
            _model.BasicConsume(queue: QUEUE_NAME, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        protected void OnReceivePaymentMessage(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<PaymentMessage>(Encoding.UTF8.GetString(args.Body.ToArray()));
                if (message == null)
                {
                    throw new Exception("Cold not process " + QUEUE_NAME + " message");
                }

                Task.Run(() => CreatePayment(message));

                _model.BasicAck(args.DeliveryTag, multiple: false);
            }
            catch (Exception)
            {
                throw;
            }

        }

        async Task CreatePayment(PaymentMessage message)
        {
            try
            {
                await Task.Delay(5000);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                    var response = await paymentService.CreatePayment(message.OrderId, message.Price, message.Discount, message.PaidValue);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
