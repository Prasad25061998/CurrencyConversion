using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Application.Commands
{
    public class AuthenticateUserCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
