using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    #region Async and non-async methods together - full set of methods

    public interface IBaseRepository<TEntity> : IBaseRepositoryAsync<TEntity>, IBaseRepositorySynchronous<TEntity>
        where TEntity : class, new()
    {
    }
    #endregion

    #region Async and common methods

    public interface IBaseRepositoryAsync<TEntity>: IBaseRepositoryCommon<TEntity>
        where TEntity : class, new()
    {
        Task<List<TEntity>> AllAsync();
        Task<TEntity> FindAsync(params object[] id);
        Task AddAsync(TEntity entity);
        
        Task RemoveAsync(params object[] id);

    }

    #endregion

    #region Only common and non-async method repos

    public interface IBaseRepositorySynchronous<TEntity>: IBaseRepositoryCommon<TEntity>
        where TEntity : class, new()
    {
    
        List<TEntity> All();
        TEntity Find(params object[] id);
        void Add(TEntity entity);
    }

    #endregion

    #region Common methods for all repos

    public interface IBaseRepositoryCommon<TEntity>
        where TEntity : class, new()
    {
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(params object[] id);
    }

    #endregion
}