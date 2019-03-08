using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.App.EF.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory
    {

        public AppRepositoryFactory()
        {
            // add to dictionary all the repo creation methods we might need!
            
            RepositoryCreationMethods.Add(typeof(IPersonRepository), 
                dataContext => new  PersonRepository(dataContext));
            
            RepositoryCreationMethods.Add(typeof(IContactRepository), 
                dataContext => new  ContactRepository(dataContext));
            
            RepositoryCreationMethods.Add(typeof(IContactTypeRepository), 
                dataContext => new  ContactTypeRepository(dataContext));
            
        }
    }
}