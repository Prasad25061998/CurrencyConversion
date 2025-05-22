using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Domain.Models
{
    public class GetConvertedCurrencyDTO
    {
        public string FromCurrency { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public string ToCurrency { get; set; }
    }
}
