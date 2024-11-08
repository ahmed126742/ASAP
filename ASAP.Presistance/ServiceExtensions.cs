using ASAP.Application.Repositories;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using ASAP.Persistence.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores;
using ASAP.Presistance.Repositores.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASAP.Presistance
{
    public static class ServiceExtensions
    {
        public static void BackgroundService(IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("InstallDirctDataConnectionString");
            services.AddDbContext<DataContext>(opt => opt.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IContractorRepository, ContractorRepository>();
            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IContractItemRepository, ContractItemRepository>();
            services.AddTransient<ISurveyRepository, SurveyRepository>();
            services.AddTransient<IFittingRepository, FittingRepository>();
            services.AddTransient<IServiceCallRepository, ServiceCallRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();


        }
    }
}