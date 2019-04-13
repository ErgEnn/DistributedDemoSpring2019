using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using Domain;

namespace BLL.App.Services
{
    public class ContactService : BaseEntityService<Contact, IAppUnitOfWork>, IContactService
    {
        public ContactService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<IEnumerable<Contact>> AllForUserAsync(int userId)
        {
            return await Uow.Contacts.AllForUserAsync(userId);
        }

        public async Task<Contact> FindForUserAsync(int id, int userId)
        {
            return await Uow.Contacts.FindForUserAsync(id, userId);
        }
    }
}