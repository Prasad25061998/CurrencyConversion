using CurrencyConversion.Domain.Contracts;
using CurrencyConversion.Domain.Models;
using CurrencyConversion.Infrastructure.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Application.Queries.GetConvertedCurrency
{
    public class GetConvertedCurrencyHandler: IRequestHandler<GetConvertedCurrency, GetConvertedCurrencyDTO> 
    {
        private readonly ICurrencyHelper _currencyHelper;
        private readonly ICurrencyDbContext _currencyDbContext;
        public GetConvertedCurrencyHandler(ICurrencyHelper currencyHelper, ICurrencyDbContext currencyDbContext)
        {
            _currencyHelper = currencyHelper;
            _currencyDbContext = currencyDbContext;
        }
        public async Task<GetConvertedCurrencyDTO> Handle(GetConvertedCurrency request, CancellationToken cancellationToken)
        {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            var getConvertedCurrency = await _currencyHelper.GetInitialAndConvertedRates(request.InitialCurrency, request.FinalCurrency, request.Amount);
            return getConvertedCurrency;
        }
    }
}
