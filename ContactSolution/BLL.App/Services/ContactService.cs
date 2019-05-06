using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.akaver.BLL.Base.Services;

namespace BLL.App.Services
{
    public class ContactService : BaseEntityService<BLL.App.DTO.Contact, DAL.App.DTO.Contact, IAppUnitOfWork>, IContactService
    {
        public ContactService(IAppUnitOfWork uow) : base(uow, new ContactMapper())
        {
            ServiceRepository = Uow.Contacts;
        }

        public async Task<List<BLL.App.DTO.Contact>> AllForUserAsync(int userId)
        {
            return (await Uow.Contacts.AllForUserAsync(userId)).Select(e => ContactMapper.MapFromInternal(e)).ToList();
        }

        public async Task<BLL.App.DTO.Contact> FindForUserAsync(int id, int userId)
        {
            return ContactMapper.MapFromInternal(await Uow.Contacts.FindForUserAsync(id, userId));
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.Contacts.BelongsToUserAsync(id, userId);
        }
    }
}