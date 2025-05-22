using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Domain.Entities
{
    public class CurrencyConversionAudit
    {
        public int Id { get; set; }
        public decimal InputAmount { get; set; }
        public string InputCurrency { get; set; }
        public decimal ConvertedAmount { get; set; }
        public string OutputCurrency { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
