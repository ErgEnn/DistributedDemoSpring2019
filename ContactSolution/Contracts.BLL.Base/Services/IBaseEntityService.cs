using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseEntityService<TEntity> : IBaseRepositoryAsync<TEntity> 
        where TEntity : class, IBaseEntity, new()
    {
        
    }
}