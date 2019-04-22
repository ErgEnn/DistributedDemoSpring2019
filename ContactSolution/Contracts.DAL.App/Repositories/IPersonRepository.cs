using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository : IBaseRepository<DALAppDTO.Person>
    {
        Task<List<DALAppDTO.Person>> AllForUserAsync(int userId);
        Task<DALAppDTO.Person> FindForUserAsync(int id, int userId);
        Task<bool> BelongsToUserAsync(int id, int userId);
    }
}