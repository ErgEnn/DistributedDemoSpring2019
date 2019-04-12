using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class ContactService : BaseEntityService<Contact, IAppUnitOfWork>, IContactService
    {
        public ContactService(IAppUnitOfWork uow) : base(uow)
        {
        }
    }
}