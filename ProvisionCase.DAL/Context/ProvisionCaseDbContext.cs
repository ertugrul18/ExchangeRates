using Microsoft.EntityFrameworkCore;
using ProvisionCase.Entities.Models;

namespace ProvisionCase.DAL.Context
{
    public class ProvisionCaseDbContext : DbContext
    {
        public DbSet<ExchangeRates> ExchangeRates { get; set; }

        public ProvisionCaseDbContext()
        {

        }

        public ProvisionCaseDbContext(DbContextOptions<ProvisionCaseDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("server=127.0.0.1;Database=Provision1;user Id=postgres;password=123");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
