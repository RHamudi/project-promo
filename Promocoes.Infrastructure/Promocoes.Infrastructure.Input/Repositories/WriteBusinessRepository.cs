using System.Data;
using Dapper;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Repositories.Interfaces;
using Promocoes.Domain.Entities;
using Promocoes.Infrastructure.Input.Queries;
using Promocoes.Infrastructure.Shared.Factory;

namespace Promocoes.Infrastructure.Input.Repositories
{
    public class WriteBusinessRepository : IWriteBusinessRepository
    {
        private readonly IDbConnection _connection;

        public WriteBusinessRepository()
        {
            _connection = SqlFactory.SqlFactoryConnection();
        }

        public void InsertBusiness(BusinessEntity business)
        {
            var query = new BusinessQueries().InsertBusinessQuery(business);

            try
            {
                using(_connection)
                {
                     _connection.Execute(query.Query, query.Parameters);
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public void UpdateUserByBusiness(string idUser, string idBusiness)
        {
            var query = new BusinessQueries().UpdateUserByBusinessQuery(idUser, idBusiness);

            try
            {
                using (_connection)
                {
                    _connection.Execute(query.Query, query.Parameters);
                }
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}