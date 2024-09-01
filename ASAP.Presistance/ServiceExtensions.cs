using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
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
            var connectionString = configuration.GetConnectionString("DataContextConnection");
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStockRepository, StockRepository>();

        }
    }
}
