using System.Reflection;
using ASAP.Application.Services;
using ASAP.Application.Services.Contract;
using ASAP.Application.Services.ContractItems;
using ASAP.Application.Services.Contractor;
using ASAP.Application.Services.Supplier;
using ASAP.Infrastructure.EmailServices;
using ASAP.Infrastructure.Services.Contract;
using ASAP.Infrastructure.Services.Supplier;
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
            services.AddTransient<ISupplierService, SupplierService>(); // Register your service
            services.AddTransient<IContractorService, ContractorService>(); // Register your service
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IContractItemsService, ContractItemService>();
            services.AddTransient<IContractService, ContractService>();

            //            services.AddTransient<IStockService, StockService>(); // Register your service
            services.AddTransient<IEmailService, EmailService>(); // Register your service

        }
    }
}
