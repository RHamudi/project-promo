using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Application.Input.Repositories.Interfaces;
using Promocoes.Errors;

namespace Promocoes.Application.Input.Receivers.UserReceiver
{
    public class Verify : IRequestHandler<VerificationCommand, OneOf<VerificationCommand, AppError>>
    {
        private readonly IWriteUserRepository _repository;

        public Verify(IWriteUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OneOf<VerificationCommand, AppError>> Handle(VerificationCommand request, CancellationToken cancellationToken)
        {
            var verifyToken = _repository.VerifyUser(request);
            
            if(verifyToken.IsT1) return verifyToken.AsT1;

            _repository.UpdateUserDate(DateTime.Now, request.Token);
            return verifyToken.AsT0;
        }
    }
}