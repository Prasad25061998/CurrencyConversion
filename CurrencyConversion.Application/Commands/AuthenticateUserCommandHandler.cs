using CurrencyConversion.Infrastructure.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Application.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, bool>
    {
        private readonly ICurrencyHelper _currencyHelper;
        private readonly IJWTTokenService _jwtTokenService;
        public AuthenticateUserCommandHandler(ICurrencyHelper currencyHelper, IJWTTokenService jwtTokenService)
        {
            _currencyHelper = currencyHelper;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<bool> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _currencyHelper.GetUserDetails(request.Username, request.Password);

            if (user == null)
                return false;

            var token =  _jwtTokenService.GenerateToken(request.Username, request.Password);

            return token != null ? true : false;
        }
    }
}
