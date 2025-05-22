using CurrencyConversion.Domain.Entities;
using CurrencyConversion.Domain.Models;
using CurrencyConversion.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyConversion.Application.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient) 
        { _httpClient = httpClient; 
        }
        public async Task<List<ExchangeRates>> GetExchangeRatesFromNationBankenAPI()
        {
            var url = "https://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRatesXML?lang=en";
            var xmlString = await _httpClient.GetStringAsync(url);

            var doc = XDocument.Parse(xmlString);
            var dailyRates = doc.Descendants("dailyrates").FirstOrDefault();

            if (dailyRates == null)
                return new List<ExchangeRates>();

            var currencyList = dailyRates.Elements("currency")
                .Select(x => new ExchangeRates
                {
                    CurrencyCode = x.Attribute("code")?.Value ?? "N/A",
                    Description = x.Attribute("desc")?.Value ?? "Unknown",
                    CurrencyRate = decimal.Parse(
                                        x.Attribute("rate")?.Value.Replace(",", "."),
                                        CultureInfo.InvariantCulture
                                   )
                })
                .ToList();

            return currencyList;
        }

    }
}


