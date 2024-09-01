using System.Reflection;
using ASAP.Application.Services;
using ASAP.Infrastructure.BackgroudServices;
using ASAP.Infrastructure.EmailServices;
using ASAP.Infrastructure.Intergrations;
using ASAP.Infrastructure.Services.Stock;
using ASAP.Infrastructure.Services.User;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASAP.Infrastructure
{
    public static class ServiceExtensions
    {

        public static void BackgroundService(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHangfire(config =>
            config.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"))); // Use your database connection string
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddHttpClient();
            services.AddTransient<IUserService, UserService>(); // Register your service
            services.AddTransient<IStockService, StockService>(); // Register your service
            services.AddTransient<IEmailService, EmailService>(); // Register your service
            services.AddSingleton<IStockBackgroundService, StockBackgroundService>(); // Register your service
            services.AddTransient<IPolygonIntergrationService, PolygonIntergration>(); // Register your service

        }
    }
}
