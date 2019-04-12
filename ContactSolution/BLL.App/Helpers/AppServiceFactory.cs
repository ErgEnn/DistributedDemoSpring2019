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
            Add<IPersonService>(uow => new PersonService(uow));
            Add<IContactService>(uow => new ContactService(uow));
            Add<IContactTypeService>(uow => new ContactTypeService(uow));
        }
    }
}