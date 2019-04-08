using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Helpers;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Helpers;

namespace DAL.App.EF.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory, IBaseRepositoryFactory
    {
        public AppRepositoryFactory()
        {
            _repositoryCreationMethodCache.Add(typeof(IContactRepository), dataContext => new ContactRepository(dataContext));
            _repositoryCreationMethodCache.Add(typeof(IContactTypeRepository), dataContext => new ContactTypeRepository(dataContext));
            _repositoryCreationMethodCache.Add(typeof(IPersonRepository), dataContext => new PersonRepository(dataContext));
           
        }
    }
    
    
}