using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TEntity> : BaseService, IBaseEntityService<TEntity> where TEntity : class, IBaseEntity<int>, new()
    {
        protected readonly IBaseUnitOfWork Uow;

        public BaseEntityService(IBaseUnitOfWork uow)
        {
            Uow = uow;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return Uow.BaseRepositoryAsync<TEntity>().Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Uow.BaseRepositoryAsync<TEntity>().Remove(entity);
        }

        public virtual void Remove(params object[] id)
        {
            Uow.BaseRepositoryAsync<TEntity>().Remove(id);
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Uow.BaseRepositoryAsync<TEntity>().AllAsync();
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            return await Uow.BaseRepositoryAsync<TEntity>().FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Uow.BaseRepositoryAsync<TEntity>().AddAsync(entity);
        }
    }
}