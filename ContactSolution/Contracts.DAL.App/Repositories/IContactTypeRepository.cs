using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactTypeRepository : IContactTypeRepository<DALAppDTO.ContactType>
    {
        Task<List<DALAppDTO.ContactTypeWithContactCounts>> GetAllWithContactCountAsync();
    }

    public interface IContactTypeRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        
    }
}