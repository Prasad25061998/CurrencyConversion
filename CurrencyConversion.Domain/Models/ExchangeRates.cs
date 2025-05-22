using System.Xml.Serialization;

namespace CurrencyConversion.Domain.Models
{
    public class ExchangeRates
    {
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public decimal CurrencyRate { get; set; }
    }
}
