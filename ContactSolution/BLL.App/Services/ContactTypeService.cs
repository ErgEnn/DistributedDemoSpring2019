using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using DAL.App.DTO;
using Domain;

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