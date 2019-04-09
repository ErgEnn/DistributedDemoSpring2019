using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using Domain;

namespace BLL.App.Services
{
    public class PersonService : BaseEntityService<Person>, IPersonService
    {
        protected readonly IAppUnitOfWork AppUnitOfWork;

        public PersonService(IBaseUnitOfWork uow) : base(uow)
        {
            AppUnitOfWork = (IAppUnitOfWork) uow;
        }
        
    }
}