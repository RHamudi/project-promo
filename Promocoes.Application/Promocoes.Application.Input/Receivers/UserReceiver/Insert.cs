using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Application.Input.Repositories.Interfaces;
using Promocoes.Application.Input.Services.SendEmail;
using Promocoes.Domain.Entities;
using Promocoes.Errors;
using Promocoes.Errors.Exceptions.infra.output;
using Promocoes.Errors.Exceptions.infra.output.User;

namespace Promocoes.Application.Input.Receivers.UserReceiver
{
    public class Insert : IRequestHandler<UserCommand, OneOf<UserEntity, AppError>>
    {
        private readonly IWriteUserRepository _repository;

        public Insert(IWriteUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OneOf<UserEntity, AppError>> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserEntity(request.Name, request.Email, request.Password, request.IdBusiness, null);

            if(!user.IsValid())
            {
                return new UserValidationsError(user.Notifications);
            }
  
            try
            {
                var result = _repository.InsertUser(user);
                _ = ClientSmtp.SendEmail(user.Email, "Por favor verifique sua conta!", user.VerificationToken);
                if (result.IsT0) return result.AsT0;
                return result;
            }
            catch 
            {
                return new InsertDbError();
            }
        }
    }
}