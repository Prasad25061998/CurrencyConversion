using CurrencyConversion.Application.Queries;
using CurrencyConversion.Application.Queries.GetConvertedCurrency;
using CurrencyConversion.Application.Queries.GetRatesQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CurrencyConversion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("rates")]
        [Authorize]
        public async Task<IActionResult> GetRates()
        {
            var result = await _mediator.Send(new GetRatesQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetConvertedCurrency([FromQuery] GetConvertedCurrency query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
