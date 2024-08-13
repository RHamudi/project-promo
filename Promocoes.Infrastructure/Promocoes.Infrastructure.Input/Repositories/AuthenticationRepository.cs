using System.Data;
using Dapper;
using OneOf;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Application.Input.Repositories.Interfaces;
using Promocoes.Errors;
using Promocoes.Errors.Exceptions.infra.output.User;
using Promocoes.Infrastructure.Input.Queries;
using Promocoes.Infrastructure.Shared.Factory;

namespace Promocoes.Infrastructure.Input.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IDbConnection _connection;

        public AuthenticationRepository()
        {
            _connection = SqlFactory.SqlFactoryConnection();
        }
        
        public OneOf<AuthenticationDTO, AppError> Authentication(AuthenticationCommand command)
        {
            var query = new UserQueries().AuthenticationQuery(command);

            try
            {
                using(_connection)
                {
                    var Auth = _connection.QueryFirstOrDefault<AuthenticationDTO>(query.Query);
                    if (Auth.Date == null) return new UserNotVerifiedError();
                    return Auth;
                }
            }
            catch
            {
                return new AuthenticationDbError(command);
            }
        }
    }
}