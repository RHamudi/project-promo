using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OneOf;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Domain.Entities;
using Promocoes.Errors;

namespace Promocoes.Application.Input.Repositories.Interfaces
{
    public interface IWriteUserRepository
    {
        OneOf<UserEntity, AppError> InsertUser(UserEntity user);
        OneOf<UpdateUserCommand, AppError> UpdateUser(UpdateUserCommand user);
        UserByIdDTO GetUserById(Guid user);
        OneOf<VerificationCommand, AppError> VerifyUser(VerificationCommand verify);
        void UpdateUserDate(DateTime date, string token);
    }
}