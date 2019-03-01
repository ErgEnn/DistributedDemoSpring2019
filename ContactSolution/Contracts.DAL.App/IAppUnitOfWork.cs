using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IPersonRepository Persons { get; }
        IContactRepository Contacts { get; }
        IContactTypeRepository ContactTypes { get; }
    }
    
}