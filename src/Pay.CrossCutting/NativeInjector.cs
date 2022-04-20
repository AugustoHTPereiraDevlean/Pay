using Microsoft.Extensions.DependencyInjection;
using Pay.Data.Connection;
using Pay.Core.Abstractions.Repositories;
using Pay.Data.Repositories;
using Pay.Core.Abstractions.Services;
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

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            // services.AddScoped<IPlanService, PlanService>();
            // services.AddScoped<IPaymentService, PaymentService>();
            // services.AddScoped<ICouponService, CouponService>();
            // services.AddScoped<IItemService, ItemService>();
            // services.AddScoped<IOrderService, OrderService>();
            // services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}