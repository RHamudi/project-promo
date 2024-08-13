using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promocoes.Errors.Exceptions.infra.output
{
    public record InsertDbError() : AppError("Erro ao consultar o servidor", "DB", null);
}
