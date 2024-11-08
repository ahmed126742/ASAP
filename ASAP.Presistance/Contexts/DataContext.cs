using ASAP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractItem> contractItems { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Fitting> Fittings { get; set; }

        public DbSet<ServiceCall> ServiceCalls { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<AttachmentHeader> AttachmentHeaders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
