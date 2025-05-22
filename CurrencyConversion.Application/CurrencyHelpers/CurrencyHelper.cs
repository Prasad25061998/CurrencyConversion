using Azure.Core;
using CurrencyConversion.Application.Queries;
using CurrencyConversion.Domain.Contracts;
using CurrencyConversion.Domain.Entities;
using CurrencyConversion.Domain.Models;
using CurrencyConversion.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConversion.Application.CurrencyHelpers
{
    public class CurrencyHelper : ICurrencyHelper
    {
        private readonly ICurrencyDbContext _dbContext;
        public CurrencyHelper(ICurrencyDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<GetRatesDTO>> GetCurrencyRates()
        {
            var getRates = await _dbContext.CurrencyData
                            .Select(rate => new GetRatesDTO
                            {
                                CurrencyCode = rate.CurrencyCode,
                                CurrencyRates = rate.CurrencyRate
                            })
                            .ToListAsync();

            return getRates;
        }

        public async Task<GetConvertedCurrencyDTO> GetInitialAndConvertedRates(string initial, string final, decimal amount)
        {
            var fromRate = await _dbContext.CurrencyData
                 .Where(c => c.CurrencyCode == initial)
                 .Select(c => c.CurrencyRate)
                 .FirstOrDefaultAsync();

            var toRate = await _dbContext.CurrencyData
                .Where(c => c.CurrencyCode == final)
                .Select(c => c.CurrencyRate)
                .FirstOrDefaultAsync();

            if (fromRate == 0 || toRate == 0)
            {
                throw new ArgumentException("Invalid currency codes or rates not found.");
            }

            
            decimal convertedAmount = amount * (toRate/fromRate);

            var conversion = new CurrencyConversionAudit
            {
                InputAmount = amount,
                InputCurrency = initial,
                ConvertedAmount = convertedAmount,
                OutputCurrency = final,
                CreatedDateTime = DateTime.UtcNow
            };

            _dbContext.CurrencyConversionAudit.Add(conversion);
            await _dbContext.SaveChangesAsync();

            return new GetConvertedCurrencyDTO
            {
                FromCurrency = initial,
                ConvertedAmount = convertedAmount,
                ToCurrency = final,
                Amount = amount
               
            };
        }
        public async Task<UserDetails> GetUserDetails(string username, string password)
        {
            return await _dbContext.UserDetails.FirstOrDefaultAsync(u =>
            u.Username == username && u.Password == password);
        }

    }
}
