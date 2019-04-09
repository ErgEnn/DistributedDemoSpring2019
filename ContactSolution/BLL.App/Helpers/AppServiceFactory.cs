using BLL.App.Services;
using BLL.Base.Helpers;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Helpers;

namespace BLL.App.Helpers
{
    public class AppServiceFactory : BaseServiceFactory
    {
        public AppServiceFactory()
        {
            // Register all your custom services here!
            ServiceCreationMethodCache.Add(typeof(IPersonService), uow => new PersonService(uow));
            ServiceCreationMethodCache.Add(typeof(IContactService), uow => new ContactService(uow));
            ServiceCreationMethodCache.Add(typeof(IContactTypeService), uow => new ContactService(uow));
        }
       
      
    }
    
}