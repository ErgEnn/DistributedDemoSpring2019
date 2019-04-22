using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Helpers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class ContactService :
        BaseEntityService<BLLAppDTO.Contact, DALAppDTO.Contact, Domain.Contact, IAppUnitOfWork>, IContactService
    {
        public ContactService(IAppUnitOfWork uow) : base(new BaseBLLMapper<BLLAppDTO.Contact, DALAppDTO.Contact>(), uow)
        {
        }

        public async Task<List<BLLAppDTO.Contact>> AllForUserAsync(int userId)
        {
            return (await Uow.Contacts.AllForUserAsync(userId)).Select(e => BaseBLLMapper.Map<BLLAppDTO.Contact>(e))
                .ToList();
        }

        public async Task<BLLAppDTO.Contact> FindForUserAsync(int id, int userId)
        {
            return BaseBLLMapper.Map<BLLAppDTO.Contact>(await Uow.Contacts.FindForUserAsync(id, userId));
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.Contacts.BelongsToUserAsync(id, userId);
        }
    }
}