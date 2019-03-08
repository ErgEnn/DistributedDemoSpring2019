using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepository : IBaseRepositoryAsync<Contact>
    {
        // add here custom methods
    }
}