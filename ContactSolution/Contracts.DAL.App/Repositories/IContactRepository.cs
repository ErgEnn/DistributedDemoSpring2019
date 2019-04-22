using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepository : IBaseRepository<DALAppDTO.Contact>
    {
        Task<List<DALAppDTO.Contact>> AllForUserAsync(int userId);
        Task<DALAppDTO.Contact> FindForUserAsync(int id, int userId);
        Task<bool> BelongsToUserAsync(int id, int userId);
        
    }
    
}