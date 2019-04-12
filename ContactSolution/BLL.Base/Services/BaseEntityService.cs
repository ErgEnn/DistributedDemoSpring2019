using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TEntity, TUnitOfWork> : IBaseEntityService<TEntity> 
        where TEntity : class, IBaseEntity, new()
        where TUnitOfWork: IUnitOfWork
    {
        protected readonly TUnitOfWork UOW;
        private readonly IBaseRepositoryAsync<TEntity> _repository;

        public BaseEntityService(TUnitOfWork uow)
        {
            UOW = uow;
            _repository = UOW.BaseRepository<TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
        }

        public void Remove(params object[] id)
        {
            _repository.Remove(id);
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _repository.AllAsync();
        }

        public async Task<TEntity> FindAsync(params object[] id)
        {
            return await _repository.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
        }
    }
}