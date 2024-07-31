using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Domain.Entities;
using Promocoes.Infrastructure.Shared.Shared;

namespace Promocoes.Infrastructure.Input.Queries
{
    public class BusinessQueries : BaseQuery
    {
        public QueryModel InsertBusinessQuery(BusinessEntity entity)
        {
            Table = Map.GetTableBusiness();

            Query = @$" 
                     
                                 INSERT INTO {Table}
                                 VALUES
                                 (
                                     @IdBusiness,
                                     @Name,
                                     @Description,
                                     @Logo,
                                     @Location,
                                     @Email,
                                     @Number,
                                     @Site,
                                     @Category,
                                     @Operation,
                                     @Geodata,
                                     @IdUser
                                 )
                                 
                     ";

            Parameters = new {
                entity.IdBusiness,
                entity.Name,
                entity.Description,
                entity.Logo,
                entity.Location,
                entity.Contacts.Email,
                entity.Contacts.Number,
                entity.Contacts.Site,
                entity.Category,
                entity.Operation,
                Geodata = entity.GeoData,
                IdUser = entity.IdUser
            };

            return new QueryModel(Query, Parameters);
        }

        public QueryModel UpdateUserByBusinessQuery(string idUser, string idBusiness)
        {
            Table = Map.GetTableUser();

            Query = $@"
                UPDATE public.tb_users
                SET idbusiness = @idBusiness
                WHERE iduser = @idUser;
            ";

            Parameters = new
            {
                idBusiness,
                idUser
            };

            return new QueryModel(Query, Parameters);
        }
    }
}