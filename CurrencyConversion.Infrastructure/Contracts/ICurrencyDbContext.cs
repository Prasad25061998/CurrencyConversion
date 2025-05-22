using CurrencyConversion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Domain.Contracts
{
    public interface ICurrencyDbContext
    {
        DbSet<CurrencyData> CurrencyData { get; set; }
        DbSet<UserDetails> UserDetails { get; set; }

        DbSet<CurrencyConversionAudit> CurrencyConversionAudit { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
