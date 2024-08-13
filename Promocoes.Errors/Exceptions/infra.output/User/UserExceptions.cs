using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promocoes.Errors.Exceptions.infra.output.User
{
    public record UserNotFoundError() : AppError("Usuario Não Encontrado", "Request", null);

    public record UserValidationsError(object data) : AppError("Verifique os dados do usuario", "Validations", data);

    public record InsertUserDbError() : AppError("Erro ao inserir usuario no servido", "Request", null);

    public record AuthenticationDbError(object data) : AppError("Erro ao realizar a authenticacao no servidor", "Request", data);

    public record UserNotVerifiedError() : AppError("Usuario Não Verificado por favor verifique seu email", "Validations", null);

    public record  VerifyUserEmailError() : AppError("Erro ao verificar usuario, por favor tente novamente", "Request", null);

    public record TokenNotFoundError() : AppError("Token Invalido", "Validations", null);

    public record TokenExistsError() : AppError("Usuario Já Verificado", "Request", null);
}
