using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using Domain;

namespace BLL.App.Services
{
    public class ContactService : BaseEntityService<Contact>, IContactService
    {
        protected readonly IAppUnitOfWork AppUnitOfWork;

        public ContactService(IBaseUnitOfWork uow) : base(uow)
        {
            AppUnitOfWork = (IAppUnitOfWork) uow;
        }
    }
}