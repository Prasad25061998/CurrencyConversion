using CurrencyConversion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Infrastructure.Contracts
{
    public interface IJWTTokenService
    {
        string GenerateToken(string username, string password);
    }
}
