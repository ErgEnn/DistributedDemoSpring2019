using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Helpers;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Helpers;

namespace DAL.App.EF.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory<AppDbContext>
    {
        public AppRepositoryFactory()
        {
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            AddToCreationMethods<IContactRepository>(dataContext => new ContactRepository(dataContext));
            AddToCreationMethods<IContactTypeRepository>(dataContext => new ContactTypeRepository(dataContext));
            AddToCreationMethods<IPersonRepository>(dataContext => new PersonRepository(dataContext));
        }
    }
    
    
}