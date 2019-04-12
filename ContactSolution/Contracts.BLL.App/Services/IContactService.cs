using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Domain;

namespace Contracts.BLL.App.Services
{
    public interface IContactService : IBaseEntityService<Contact>, IContactRepository
    {
        
    }
}