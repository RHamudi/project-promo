using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Domain.Entities;

namespace Promocoes.Application.Input.Repositories.Interfaces
{
    public interface IWriteBusinessRepository
    {
        void InsertBusiness(BusinessEntity business);
        void UpdateUserByBusiness(string idUser, string idBusiness);
    }
}