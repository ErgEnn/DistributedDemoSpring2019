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
            ServiceCreationMethods.Add(typeof(IPersonService), uow => new PersonService(uow));
            ServiceCreationMethods.Add(typeof(IContactService), uow => new ContactService(uow));
            ServiceCreationMethods.Add(typeof(IContactTypeService), uow => new ContactTypeService(uow));
        }
    }
}