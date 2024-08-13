using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Domain.Entities;
using Promocoes.Infrastructure.Shared.Shared;

namespace Promocoes.Infrastructure.Input.Queries
{
    public class UserQueries : BaseQuery
    {
        public QueryModel InsertUser(UserEntity entity)
        {
            this.Table = Map.GetTableUser();

            this.Query = @$"
            INSERT INTO {this.Table}
            VALUES
            (
                @IdUser,
                @Name,
                @Email,
                @Password,
                @IdBusiness,
                @VerificationToken,
                @VerifietAt
            )
            ";

            this.Parameters = new {
                IdUser = entity.IdUser,
                Name = entity.Name,
                Email = entity.Email,
                Password = entity.Password,
                IdBusiness = entity.IdBusiness.Length == 0 ? null : entity.IdBusiness,
                VerificationToken = entity.VerificationToken,
                VerifietAt = entity.VerifietAt
            };

            return new QueryModel(this.Query, this.Parameters);
        }

        public QueryModel GetUserById(Guid command)
        {
            this.Table = Map.GetTableUser();

            this.Query = $@"
                        SELECT
                            tb.IdUser,
                            tb.Name,
                            tb.Email,
                            tb.Password,
                            tb.IdBusiness
                        FROM
                            tb_users tb WHERE IdUser = @IdUser
                        ";
            
            this.Parameters = new {
                IdUser = command
            };

            return new QueryModel(this.Query, this.Parameters);
        }

        public QueryModel UpdateUser(UpdateUserCommand command)
        {
            this.Table = Map.GetTableUser();

            this.Query = $@"
                        UPDATE {this.Table}
                        SET Name = '{command.Name}', Email = '{command.Email}', Password = '{command.Password}'
                        WHERE IdUser = '{command.IdUser}'
                        ";
            
            this.Parameters = new {
                Name = command.Name,
                Email = command.Email,
                Password = command.Password,
                IdUser = command.IdUser
            };

            return new QueryModel(this.Query, null);
        }

        public QueryModel AuthenticationQuery(AuthenticationCommand command)
        {
            this.Table = Map.GetTableUser();

            this.Query = $@"

                            SELECT
                                	tb.IdUser as IdUsuario,
                                    tb.Name as Nome,
                                    tb.Email as Email,
                                    tb.Password as Senha,
                                    tb.IdBusiness as IdEmpresa,
                                    tb.VerificationToken as Token,
                                    tb.VerifietAt as Date
                            FROM
                            {this.Table} tb WHERE Email = '{command.Email}' AND Password = '{command.Password}'
                            
                        
                          ";

            this.Parameters = new
            {
                Email = command.Email,
                Password = command.Password,
            };

            return new QueryModel(this.Query, this.Parameters);
        }

        public QueryModel VerifyTokenQuery(string Token)
        {
            this.Table = Map.GetTableUser();

            this.Query = $@"
                            SELECT 
                                tb.VerificationToken 
                            FROM
                                tb_users tb 
                            WHERE tb.VerificationToken = '{Token}'
            ";

            return new QueryModel(this.Query, null);
        }

        public QueryModel UpdateVerifyDate(DateTime date, string token)
        {
            this.Table = Map.GetTableUser();

            this.Query = $@"
                    UPDATE tb_users
                    SET VerifietAt = '{date}'
                    WHERE VerificationToken = '{token} AND VerifietAt IS NULL'
            ";
            
            this.Parameters = null;

            return new QueryModel(this.Query, this.Parameters);
        }
    }
}