using CurrencyConversion.Domain.Models;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace CurrencyConversion.Application.Queries.GetRatesQuery
{
    public class GetRatesQuery : IRequest<List<GetRatesDTO>> { }

}
