using CurrencyConversion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Infrastructure.Contracts
{
    public interface ICurrencyBackgroundHourlyJob
    {
        Task Run();
    }
}
