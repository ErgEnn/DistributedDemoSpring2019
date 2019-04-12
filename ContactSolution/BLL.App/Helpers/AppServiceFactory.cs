using BLL.App.Services;
using BLL.Base.Helpers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Helpers
{
    public class AppServiceFactory : BaseServiceFactory<IAppUnitOfWork>
    {
        public AppServiceFactory()
        {
            RegisterAllCreationMethods();
        }

        private void RegisterAllCreationMethods()
        {
            AddCreationMethod<IPersonService>(uow => new PersonService(uow));
            AddCreationMethod<IContactService>(uow => new ContactService(uow));
            AddCreationMethod<IContactTypeService>(uow => new ContactTypeService(uow));
        }
        
    }
}