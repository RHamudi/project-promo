using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Application.Input.Repositories.Interfaces;
using Promocoes.Application.Input.Services.Jwt;
using Promocoes.Errors;
using Promocoes.Errors.Exceptions.infra.output.User;

namespace Promocoes.Application.Input.Receivers.BusinessReceiver
{
    public class Authentication : IRequestHandler<AuthenticationCommand, OneOf<AuthenticationDTO, AppError>>
    {
        private readonly IAuthenticationRepository _repository;

        public Authentication(IAuthenticationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OneOf<AuthenticationDTO, AppError>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {

            var command = _repository.Authentication(request);

            if (command.IsT1)
                return command.AsT1;

            var createToken = new CreateToken(request);
            command.AsT0.Token = createToken.Token;
            return command.AsT0;

            /*
            try
            {
                //if(command.Date == null) return Task.FromResult(new State(401,"Usuario não verificado, por favor verifique seu email", null));
                if (command != null)
                {
                    var create = new CreateToken(request);
                    return Task.FromResult(new State(200, "Usuario autenticado com sucesso", new {
                        Authentication = create.Token,
                        User = command
                    }));
                }

                throw new Exception();

            }
            catch
            {
                return Task.FromResult(new State(400, "Credenciais incorretas", null));
            }
            *
             *
             */
        }
    }
}