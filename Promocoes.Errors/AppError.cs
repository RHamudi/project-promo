using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promocoes.Errors
{
    public record AppError(string Detail, string TypeError, object? data);
}
