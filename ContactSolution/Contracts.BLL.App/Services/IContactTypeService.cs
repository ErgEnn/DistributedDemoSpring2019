using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IContactTypeService : IBaseEntityService<BLLAppDTO.ContactType>, IContactTypeRepository<BLLAppDTO.ContactType>
    {
        Task<List<BLLAppDTO.ContactTypeWithContactCounts>> GetAllWithContactCountAsync();
    }
}