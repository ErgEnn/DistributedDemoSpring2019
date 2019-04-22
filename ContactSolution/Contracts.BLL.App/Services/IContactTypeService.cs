using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Domain;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO; 

namespace Contracts.BLL.App.Services
{
    public interface IContactTypeService : IBaseEntityService<BLLAppDTO.ContactType>
    
    {
        Task<List<BLLAppDTO.ContactTypeContactCount>> GetAllWithContactCountAsync();
    
    }
}