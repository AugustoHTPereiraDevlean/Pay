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
    public class SubscriptionBackgroundJob : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer = null!;

        public SubscriptionBackgroundJob(IServiceProvider serviceProvider)
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
            Task.Run(() => ProcessSubscriptions());
        }

        async Task ProcessSubscriptions()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var subscriptionRepository = scope.ServiceProvider.GetRequiredService<ISubscriptionRepository>();
                    var subscriptions = await subscriptionRepository.SelectActivedAsync();
                    if (subscriptions != null && subscriptions.Any())
                    {
                        var subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();
                        await subscriptionService.ProcessAsync(subscriptions);
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
