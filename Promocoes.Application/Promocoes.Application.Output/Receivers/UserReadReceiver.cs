using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Application.Output.DTOs;
using Promocoes.Application.Output.Interfaces;
using Promocoes.Errors;
using Promocoes.Errors.Exceptions.infra.output;

namespace Promocoes.Application.Output.Receivers
{
    public class UserReadReceiver : IRequestHandler<UserDTO, State>
    {
        private readonly IReadUserRepository _repository;

        public UserReadReceiver(IReadUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<State> Handle(UserDTO request, CancellationToken cancellationToken)
        {
            var getUsers = _repository.GetAllUsers();

           return await Task.FromResult(new State(200, "Usuarios coletados com sucesso", getUsers));
        }
    }
}