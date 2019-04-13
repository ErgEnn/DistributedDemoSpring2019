using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TEntity, TUnitOfWork> : BaseService, IBaseEntityService<TEntity> where TEntity : class, IBaseEntity, new()
    where TUnitOfWork: IBaseUnitOfWork
    {
        protected readonly TUnitOfWork Uow;
        private readonly IBaseRepositoryAsync<TEntity> _repo; 

        public BaseEntityService(TUnitOfWork uow)
        {
            Uow = uow;
            _repo = Uow.BaseRepository<TEntity>();
        }

        public virtual TEntity Update(TEntity entity)
        {
            return _repo.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _repo.Remove(entity);
        }

        public virtual void Remove(params object[] id)
        {
            _repo.Remove(id);
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _repo.AllAsync();
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            return await _repo.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _repo.AddAsync(entity);
        }
    }
}