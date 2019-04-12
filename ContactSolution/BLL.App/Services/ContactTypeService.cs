using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class ContactTypeService : BaseEntityService<ContactType, IAppUnitOfWork>, IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork uow) : base(uow)
        {
        }
    }
}