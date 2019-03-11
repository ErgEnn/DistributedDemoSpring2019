using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IContactRepository Contacts { get; }
        IContactTypeRepository ContactTypes { get; }
        IPersonRepository Persons { get; }
    }
}