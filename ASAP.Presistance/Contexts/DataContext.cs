using ASAP.Domain.Entities;
using ASAP.Presistance.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
