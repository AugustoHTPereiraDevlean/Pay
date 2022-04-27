using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Abstractions.Services;

namespace Pay.Infra.BackgroundJob.Jobs
{
    public class CreditCardPaymentBackgroundJob : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer = null!;

        public CreditCardPaymentBackgroundJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        void DoWork(object? state)
        {
            Task.Run(() => ProcessPayments());
        }

        async Task ProcessPayments()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var paymentRepository = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();
                    var payments = await paymentRepository.SelectCreditCardWaitingPayment();
                    if (payments != null && payments.Any())
                    {
                        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                        await paymentService.ProcessWaitingPayments(payments);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
