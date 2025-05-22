using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    namespace CurrencyConversion.Infrastructure
    {
        public class DesignTimeCurrencyDbContextFactory : IDesignTimeDbContextFactory<CurrencyDbContext>
        {
            public CurrencyDbContext CreateDbContext(string[] args)
            {
                // Build config to access connection string
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())  // required for CLI tools
                    .AddJsonFile("appsettings.json")
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<CurrencyDbContext>();
                var connectionString = config.GetConnectionString("ConnectionString");

                optionsBuilder.UseSqlServer(connectionString);

                return new CurrencyDbContext(optionsBuilder.Options);
            }
        }
    }
}
