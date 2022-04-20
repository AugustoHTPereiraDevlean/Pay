using Microsoft.Extensions.DependencyInjection;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Abstractions.Services;
using Pay.Data.Connection;
using Pay.Data.Repositories;

namespace Pay.CrossCutting
{
    public static class NativeInjector
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, string connectionString)
        {
            var options = new SqlServerOptions(connectionString);
            services.AddSingleton(options);

            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionHistoricRepository, SubscriptionHistoricRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentHistoricRepository, PaymentHistoricRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}