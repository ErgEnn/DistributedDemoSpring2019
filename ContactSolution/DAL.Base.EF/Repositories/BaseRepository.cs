using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TDALEntity, TDomainEntity, TDbContext> :
        BaseRepository<TDALEntity, TDomainEntity, TDbContext, int>
        where TDALEntity : class, new()
        where TDomainEntity : class, IDomainEntity, new()
        where TDbContext : DbContext
    {
        public BaseRepository(TDbContext repositoryDbContext, IBaseDALMapper mapper) : base(repositoryDbContext, mapper)
        {
        }
    }


    public class BaseRepository<TDALEntity, TDomainEntity, TDbContext, TKey> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
        where TDomainEntity : class, IDomainEntity<TKey>, new()
        where TDbContext : DbContext
        where TKey : IComparable
    {
        protected readonly TDbContext RepositoryDbContext;
        protected readonly DbSet<TDomainEntity> RepositoryDbSet;

        private readonly IBaseDALMapper _mapper;

        public BaseRepository(TDbContext repositoryDbContext, IBaseDALMapper mapper)
        {
            RepositoryDbContext = repositoryDbContext;
            _mapper = mapper;
            // get the dbset by type from db context
            RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
        }


        public virtual TDALEntity Update(TDALEntity entity)
        {
            return _mapper.Map<TDALEntity>(RepositoryDbSet.Update(_mapper.Map<TDomainEntity>(entity)).Entity);
        }

        public virtual void Remove(TDALEntity entity)
        {
            RepositoryDbSet.Remove(_mapper.Map<TDomainEntity>(entity));
        }

        public virtual void Remove(params object[] id)
        {
            RepositoryDbSet.Remove(RepositoryDbSet.Find(id));
        }

        public virtual async Task<List<TDALEntity>> AllAsync()
        {
            return (await RepositoryDbSet.ToListAsync())
                .Select(e => _mapper.Map<TDALEntity>(e)).ToList();
        }

        public virtual async Task<TDALEntity> FindAsync(params object[] id)
        {
            return _mapper.Map<TDALEntity>( (await RepositoryDbSet.FindAsync(id)));
        }

        public virtual async Task AddAsync(TDALEntity entity)
        {
            await RepositoryDbSet.AddAsync(_mapper.Map<TDomainEntity>(entity));
        }

        public List<TDALEntity> All()
        {
            return RepositoryDbSet.Select(e => _mapper.Map<TDALEntity>(e)).ToList();
        }

        public TDALEntity Find(params object[] id)
        {
            return _mapper.Map<TDALEntity>(RepositoryDbSet.Find(id));
        }

        public void Add(TDALEntity entity)
        {
            RepositoryDbSet.Add(_mapper.Map<TDomainEntity>(entity));
        }
    }
}