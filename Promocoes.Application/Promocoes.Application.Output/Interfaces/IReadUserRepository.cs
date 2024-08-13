using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneOf;
using Promocoes.Application.Output.DTOs;
using Promocoes.Errors;

namespace Promocoes.Application.Output.Interfaces
{
    public interface IReadUserRepository
    {
        IEnumerable<UserDTO> GetAllUsers();
        OneOf<UserDTO, AppError> GetUserById(Guid user);
    }
}