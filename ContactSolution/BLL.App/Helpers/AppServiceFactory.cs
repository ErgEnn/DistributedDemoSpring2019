using BLL.App.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.akaver.BLL.Base.Helpers;

namespace BLL.App.Helpers
{
    public class AppServiceFactory : BaseServiceFactory<IAppUnitOfWork>
    {
        public AppServiceFactory()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            // Register all your custom services here!
            AddToCreationMethods<IPersonService>(uow => new PersonService(uow));
            AddToCreationMethods<IContactService>(uow => new ContactService(uow));
            AddToCreationMethods<IContactTypeService>(uow => new ContactTypeService(uow));
        }

    }
    
}