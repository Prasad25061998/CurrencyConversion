using CurrencyConversion.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Application.Queries.GetConvertedCurrency
{
    public class GetConvertedCurrency : IRequest<GetConvertedCurrencyDTO>
    {
        public decimal Amount { get; set; }
        public string InitialCurrency { get; set; }
        public string FinalCurrency { get; set; }
    }
}
