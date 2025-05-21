using CurrencyConversion.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CurrencyConversion.Application.Commands;

namespace CurrencyConversion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand request)
        {
            bool isAuthenticated  = await _mediator.Send(request);

            return Ok(isAuthenticated);
        }
    }
}
