using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Application.Input.Receivers;
using Promocoes.Errors;

namespace Promocoes.Application.Input.Commands.BusinessContext
{
    public class AuthenticationCommand : IRequest<OneOf<AuthenticationDTO, AppError>>
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}