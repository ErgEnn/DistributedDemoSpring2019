using Contracts.DAL.App.Repositories;
using ee.itcollege.akaver.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IContactRepository Contacts { get; }
        IContactTypeRepository ContactTypes { get; }
        IPersonRepository Persons { get; }
    }
}