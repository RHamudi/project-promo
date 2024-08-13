using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Errors;


namespace Promocoes.Application.Output.DTOs
{
    public class UserDTO : IRequest<State>
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid? IdEmpresa { get; set; }
    }
}