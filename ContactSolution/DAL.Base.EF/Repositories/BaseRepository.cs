using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TDALEntity, TDomainEntity, TDbContext> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
        where TDomainEntity : class, IBaseEntity, new()
        //where TMapper: IBaseMapper<TEntity, TInternalEntity>
        where TDbContext : DbContext
    {
        protected readonly DbContext RepositoryDbContext;
        protected readonly DbSet<TDomainEntity> RepositoryDbSet;
        protected readonly IBaseDALMapper BaseDALMapper;

        
        public BaseRepository(IBaseDALMapper baseDALMapper, TDbContext repositoryDbContext)
        {
            RepositoryDbContext = repositoryDbContext;
            BaseDALMapper = baseDALMapper;
            // get the dbset by type from db context
            RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
        }


        public virtual TDALEntity Update(TDALEntity entity)
        {
            return BaseDALMapper.Map<TDALEntity>(
                RepositoryDbSet.Update(BaseDALMapper.Map<TDomainEntity>(entity)).Entity);
        }

        public virtual void Remove(TDALEntity entity)
        {
            RepositoryDbSet.Remove(BaseDALMapper.Map<TDomainEntity>(entity));
        }

        public virtual void Remove(params object[] id)
        {
            RepositoryDbSet.Remove(RepositoryDbSet.Find(id));
        }

        public virtual async Task<List<TDALEntity>> AllAsync()
        {
            return await RepositoryDbSet.Select(e => BaseDALMapper.Map<TDALEntity>(e)).ToListAsync();
        }

        public virtual async Task<TDALEntity> FindAsync(params object[] id)
        {
            return BaseDALMapper.Map<TDALEntity>(await RepositoryDbSet.FindAsync(id));
        }

        public virtual async Task AddAsync(TDALEntity entity)
        {
            await RepositoryDbSet.AddAsync(BaseDALMapper.Map<TDomainEntity>(entity));
        }

        public virtual async Task RemoveAsync(params object[] id)
        {
            RepositoryDbSet.Remove(await RepositoryDbSet.FindAsync(id));
        }

        public List<TDALEntity> All()
        {
            return RepositoryDbSet.Select(e => BaseDALMapper.Map<TDALEntity>(e)).ToList();
        }

        public TDALEntity Find(params object[] id)
        {
            return BaseDALMapper.Map<TDALEntity>(RepositoryDbSet.Find(id));
        }

        public void Add(TDALEntity entity)
        {
            RepositoryDbSet.Add(BaseDALMapper.Map<TDomainEntity>(entity));
        }
    }
}