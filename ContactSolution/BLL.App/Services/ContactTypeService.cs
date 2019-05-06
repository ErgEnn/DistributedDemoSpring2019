using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.akaver.BLL.Base.Services;

namespace BLL.App.Services
{
    public class ContactTypeService : BaseEntityService<BLL.App.DTO.ContactType, DAL.App.DTO.ContactType, IAppUnitOfWork>, IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork uow) : base(uow, new ContactTypeMapper())
        {
            ServiceRepository = Uow.ContactTypes;
        }

        public async Task<List<BLL.App.DTO.ContactTypeWithContactCounts>> GetAllWithContactCountAsync()
        {
            return (await Uow.ContactTypes.GetAllWithContactCountAsync()).Select(e => ContactTypeMapper.MapFromDAL(e)).ToList();
        }
    }
}