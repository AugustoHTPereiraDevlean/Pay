using Microsoft.Extensions.DependencyInjection;
using Pay.Core.Abstractions.Queue;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Abstractions.Services;
using Pay.Data.Connection;
using Pay.Data.Repositories;
using Pay.Infra.Queue;
using Pay.Infra.Queue.Workers;
using Pay.Services;

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
            services.AddScoped<ICouponPlanUserRepository, CouponPlanUserRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IPaymentService, PaymentService>();
            // services.AddScoped<IPlanService, PlanService>();
            // services.AddScoped<ICouponService, CouponService>();
            // services.AddScoped<IItemService, ItemService>();
            // services.AddScoped<IOrderService, OrderService>();
            // services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection RegisterMessaging(this IServiceCollection services, QueueOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            services.AddSingleton(options);

            services.AddScoped<ISender, QueueSender>();
            services.AddHostedService<PaymentConsumer>();

            return services;
        }
    }
}