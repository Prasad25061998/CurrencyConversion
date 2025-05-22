using CurrencyConversion.Domain.Contracts;
using CurrencyConversion.Domain.Entities;
using CurrencyConversion.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CurrencyConversion.Infrastructure
{
    public class CurrencyDbContext : DbContext, ICurrencyDbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
            : base(options)
        {
        }

        public DbSet<CurrencyData> CurrencyData { get; set; }
        public DbSet<CurrencyConversionAudit> CurrencyConversionAudit { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyDataConfiguration());

        }
    }
}