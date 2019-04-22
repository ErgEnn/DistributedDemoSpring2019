using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactTypeRepository : IBaseRepository<DALAppDTO.ContactType>
    {      
        Task<List<DALAppDTO.ContactTypeContactCount>> GetAllWithContactCountAsync();
    }
}