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
    public class UpdateUserCommand : IRequest<OneOf<UpdateUserCommand, AppError>>
    {
        public Guid IdUser { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? IdBusiness { get; set; }
    }
}