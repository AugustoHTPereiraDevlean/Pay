using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay.Infra.BackgroundJob.Jobs
{
    public class BankslipPaymentBackgroundJob : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer = null!;

        public BankslipPaymentBackgroundJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(3));

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
                    var payments = await paymentRepository.SelectBankslipWaitingPayment();
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
