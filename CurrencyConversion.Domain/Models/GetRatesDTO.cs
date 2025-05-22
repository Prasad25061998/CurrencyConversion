using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Domain.Models
{
    public class GetRatesDTO
    {
        public string CurrencyCode { get; set; }
        public decimal CurrencyRates { get; set; }
    }

}
