using System;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base
{
    public interface IBaseBLL
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        
        IBaseEntityService<TEntity> BaseService<TEntity>() where TEntity :  class, IBaseEntity, new();

    }
}