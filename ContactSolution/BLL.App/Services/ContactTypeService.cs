using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Helpers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO; 

namespace BLL.App.Services
{
    public class ContactTypeService : BaseEntityService<BLLAppDTO.ContactType, DALAppDTO.ContactType, Domain.ContactType, IAppUnitOfWork>, IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork uow) : base(new BaseBLLMapper<BLLAppDTO.ContactType, DALAppDTO.ContactType>() , uow)
        {
        }

        public async Task<List<BLLAppDTO.ContactTypeContactCount>> GetAllWithContactCountAsync()
        {
            return  (await Uow.ContactTypes.GetAllWithContactCountAsync()).Select(e => BaseBLLMapper.Map<BLLAppDTO.ContactTypeContactCount>(e)).ToList();
        }
    }
}