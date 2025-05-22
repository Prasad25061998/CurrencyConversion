using CurrencyConversion.Domain.Contracts;
using CurrencyConversion.Domain.Entities;
using CurrencyConversion.Domain.Models;
using CurrencyConversion.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyConversion.Infrastructure
{
    public class CurrencyBackgroundHourlyJob : ICurrencyBackgroundHourlyJob
    {
        private readonly ICurrencyHelper _currencyHelper;
        private readonly IHttpClientService _httpClientService;
        private readonly ICurrencyDbContext _currencyDbContext;
        public CurrencyBackgroundHourlyJob(ICurrencyHelper currencyHelper, IHttpClientService httpClientService, ICurrencyDbContext currencyDbContext)
        {
            _currencyHelper = currencyHelper;
            _httpClientService = httpClientService;
            _currencyDbContext = currencyDbContext;
        }
        public async Task Run()
        {
            var apiDetails = await _httpClientService.GetExchangeRatesFromNationBankenAPI();

            // Get all records from CurrencyData table
            var existingCurrencyData = await _currencyDbContext.CurrencyData.ToListAsync();

            if (existingCurrencyData.Count == 0)
            {
                // No records in DB, insert everything from API
                var newCurrencyData = apiDetails.Select(api => new CurrencyData
                {
                    CurrencyCode = api.CurrencyCode,
                    CreatedDateTime = DateTime.UtcNow,
                    CurrencyRate = api.CurrencyRate,
                    Description = api.Description
                }).ToList();

                await _currencyDbContext.CurrencyData.AddRangeAsync(newCurrencyData);
                await _currencyDbContext.SaveChangesAsync();
            }
            else
            {
                // Records exist, update only changed ones
                var existingDict = existingCurrencyData.ToDictionary(x => x.CurrencyCode);
                bool updated = false;

                foreach (var api in apiDetails)
                {
                    if (existingDict.TryGetValue(api.CurrencyCode, out var existing))
                    {
                        if (existing.CurrencyRate != api.CurrencyRate)
                        {
                            existing.CurrencyRate = api.CurrencyRate;
                            existing.UpdatedDateTime = DateTime.UtcNow;
                            updated = true;
                        }
                    }
                }

                if (updated)
                {
                    await _currencyDbContext.SaveChangesAsync();
                }
            }
        }
    }
    
}
