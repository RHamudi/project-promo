using System.Data;
using Dapper;
using OneOf;
using Promocoes.Application.Output.DTOs;
using Promocoes.Application.Output.Interfaces;
using Promocoes.Errors;
using Promocoes.Errors.Exceptions.infra.output;
using Promocoes.Errors.Exceptions.infra.output.User;
using Promocoes.Infrastructure.Output.Queries;
using Promocoes.Infrastructure.Shared.Factory;

namespace Promocoes.Infrastructure.Output.Repositories
{
    public class ReadUserRepository : IReadUserRepository
    {
        private readonly IDbConnection _connection;

        public ReadUserRepository()
        {
            _connection = SqlFactory.SqlFactoryConnection();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var query = new UserQueries().GetAllUsers();

            try
            {
                using(_connection)
                {
                    return  _connection.Query<UserDTO>(query.Query, query.Parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OneOf<UserDTO, AppError> GetUserById(Guid user)
        {
            var query = new UserQueries().GetUserById(user);

            
            using(_connection)
            {
                var result = _connection.QueryFirstOrDefault<UserDTO>(query.Query, query.Parameters);
                if (result == null) return new UserNotFoundError();
                return result;
            }
            
        }
    }
}