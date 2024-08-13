using OneOf;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Errors;

namespace Promocoes.Application.Input.Repositories.Interfaces
{
    public interface IAuthenticationRepository
    {
        OneOf<AuthenticationDTO, AppError> Authentication(AuthenticationCommand command);
    }
}