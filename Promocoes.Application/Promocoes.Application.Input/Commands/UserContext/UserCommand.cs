using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Application.Input.Receivers;
using Promocoes.Domain.Entities;
using Promocoes.Errors;

namespace Promocoes.Application.Input.Commands.UserContext
{
    public class UserCommand : IRequest<OneOf<UserEntity, AppError>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IdBusiness { get; set; } 
    }
}