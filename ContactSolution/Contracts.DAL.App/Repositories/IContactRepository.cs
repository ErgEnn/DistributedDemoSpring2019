using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.akaver.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IContactRepository : IContactRepository<DALAppDTO.Contact>
    {
    }

    public interface IContactRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {
        Task<List<TDALEntity>> AllForUserAsync(int userId);
        Task<TDALEntity> FindForUserAsync(int id, int userId);
        Task<bool> BelongsToUserAsync(int id, int userId);
        
    }
    
}