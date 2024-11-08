using System.Reflection;
using ASAP.Application.Services;
using ASAP.Application.Services.Contract;
using ASAP.Application.Services.ContractItems;
using ASAP.Application.Services.Contractor;
using ASAP.Application.Services.Supplier;
using ASAP.Application.Services.TaskStatus;
using ASAP.Application.Services.User.Fitting;
using ASAP.Application.Services.User.ServiceCall;
using ASAP.Application.Services.User.Survey;
using ASAP.Domain.Entities;
using ASAP.Infrastructure.EmailServices;
using ASAP.Infrastructure.Services.Contract;
using ASAP.Infrastructure.Services.Fitter;
using ASAP.Infrastructure.Services.ServiceCall;
using ASAP.Infrastructure.Services.Supplier;
using ASAP.Infrastructure.Services.Surveyor;
using ASAP.Infrastructure.Services.TaskStatus;
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
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddTransient<IFitterService, FitterService>();
            services.AddTransient<IServiceCallService, ServiceCallService>();
            services.AddTransient<ITaskStatusService, TaskStatusService>();

            //            services.AddTransient<IStockService, StockService>(); // Register your service
            services.AddTransient<IEmailService, EmailService>(); // Register your service

        }
    }
}
