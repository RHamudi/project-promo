using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Application.Input.Receivers;
using Promocoes.Errors;

namespace Promocoes.Application.Input.Commands.UserContext
{
    public class VerificationCommand : IRequest<OneOf<VerificationCommand, AppError>>
    {
        public string Token { get; set; }
    }
}