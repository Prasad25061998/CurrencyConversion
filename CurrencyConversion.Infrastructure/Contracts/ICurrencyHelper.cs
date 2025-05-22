using CurrencyConversion.Domain.Entities;
using CurrencyConversion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Infrastructure.Contracts
{
    public interface ICurrencyHelper
    {
        Task<List<GetRatesDTO>> GetCurrencyRates();
        Task<GetConvertedCurrencyDTO> GetInitialAndConvertedRates(string initial, string final, decimal amount);
        Task<UserDetails> GetUserDetails(string username, string password);
    }
}
