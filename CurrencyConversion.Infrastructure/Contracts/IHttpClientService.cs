using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using CurrencyConversion.Domain.Models;


namespace CurrencyConversion.Infrastructure.Contracts
{
    public interface IHttpClientService
    {
        Task<List<ExchangeRates>> GetExchangeRatesFromNationBankenAPI();
    }
}
