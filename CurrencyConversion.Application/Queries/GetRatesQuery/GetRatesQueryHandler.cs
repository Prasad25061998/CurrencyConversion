using CurrencyConversion.Domain.Models;
using CurrencyConversion.Infrastructure.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Application.Queries.GetRatesQuery
{
    public class GetRatesQueryHandler : IRequestHandler<GetRatesQuery, List<GetRatesDTO>>
    {
        private readonly ICurrencyHelper _currencyHelper;

        public GetRatesQueryHandler(ICurrencyHelper helper)
        {
            _currencyHelper = helper;
        }

        public async Task<List<GetRatesDTO>> Handle(GetRatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getRates = await _currencyHelper.GetCurrencyRates();
                if (getRates == null || getRates.Count() == 0)
                {
                    return null;
                }
                return getRates;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null; 
            }
        }
    }

}
