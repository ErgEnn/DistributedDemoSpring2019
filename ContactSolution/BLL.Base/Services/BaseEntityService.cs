using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.Base.Helpers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TBLLEntity, TDALEntity, TDomainEntity, TUnitOfWork> : BaseService, IBaseEntityService<TBLLEntity>
        where TBLLEntity : class, new()
        where TDALEntity : class, new()
        where TDomainEntity : class, IBaseEntity, new()
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly TUnitOfWork Uow;
        private readonly IBaseRepository<TDALEntity> _repo;
        protected readonly IBaseBLLMapper BaseBLLMapper;
        public BaseEntityService(IBaseBLLMapper baseBLLMapper, TUnitOfWork uow)
        {
            Uow = uow;
            BaseBLLMapper = baseBLLMapper;
            _repo = Uow.BaseRepository<TDALEntity, TDomainEntity>();
        }

        public virtual TBLLEntity Update(TBLLEntity entity)
        {
            return BaseBLLMapper.Map<TBLLEntity>(_repo.Update(BaseBLLMapper.Map<TDALEntity>(entity)));
        }

        public virtual void Remove(TBLLEntity entity)
        {
            _repo.Remove(BaseBLLMapper.Map<TDALEntity>(entity));
        }

        public virtual void Remove(params object[] id)
        {
            _repo.Remove(id);
        }

        public virtual async Task<List<TBLLEntity>> AllAsync()
        {
            return (await _repo.AllAsync()).Select(e => BaseBLLMapper.Map<TBLLEntity>(e)).ToList();
        }

        public virtual async Task<TBLLEntity> FindAsync(params object[] id)
        {
            return BaseBLLMapper.Map<TBLLEntity>(await _repo.FindAsync(id));
        }

        public virtual async Task AddAsync(TBLLEntity entity)
        {
            await _repo.AddAsync(BaseBLLMapper.Map<TDALEntity>(entity));
        }

        public virtual async Task RemoveAsync(params object[] id)
        {
            await _repo.RemoveAsync(id);
        }

        public List<TBLLEntity> All()
        {
            return _repo.All().Select(e => BaseBLLMapper.Map<TBLLEntity>(e)).ToList();
        }

        public TBLLEntity Find(params object[] id)
        {
            return BaseBLLMapper.Map<TBLLEntity>(_repo.Find(id));
        }

        public void Add(TBLLEntity entity)
        {
            _repo.Add(BaseBLLMapper.Map<TDALEntity>(entity));
        }
    }
}