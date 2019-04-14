using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    #region Async and non-async methods together - full set of methods

    public interface IBaseRepository<TEntity> : IBaseRepositoryAsync<TEntity>, IBaseRepositorySynchronous<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
    }

    public interface IBaseRepository<TEntity, TKey> : IBaseRepositoryAsync<TEntity, TKey>,
        IBaseRepositorySynchronous<TEntity, TKey>
        where TKey : IComparable
        where TEntity : class, IBaseEntity<TKey>, new()
    {
    }

    #endregion

    #region Async and common methods

    public interface IBaseRepositoryAsync<TEntity> : IBaseRepositoryAsync<TEntity, int>
        where TEntity : class, IBaseEntity<int>, new()
    {
    }

    public interface IBaseRepositoryAsync<TEntity, TKey> : IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TKey : IComparable
    {
        Task<List<TEntity>> AllAsync();
        Task<TEntity> FindAsync(params object[] id);
        Task AddAsync(TEntity entity);
    }

    #endregion

    #region Only common and non-async method repos

    public interface IBaseRepositorySynchronous<TEntity> : IBaseRepositorySynchronous<TEntity, int>
        where TEntity : class, IBaseEntity<int>, new()
    {
    }

    public interface IBaseRepositorySynchronous<TEntity, TKey> : IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TKey : IComparable
    {
        List<TEntity> All();
        TEntity Find(params object[] id);
        void Add(TEntity entity);
    }

    #endregion

    #region Common methods for all repos

    public interface IBaseRepositoryCommon<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
        where TKey : IComparable
    {
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(params object[] id);
    }

    #endregion
}