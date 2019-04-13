using BLL.App.Services;
using BLL.Base.Helpers;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.App;

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