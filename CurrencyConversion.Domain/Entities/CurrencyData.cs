using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Domain.Entities
{
    public class CurrencyData
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public decimal CurrencyRate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
