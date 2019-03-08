using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        // repo cache
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();

        public IPersonRepository Persons =>
            GetOrCreateRepository(dataContext => new PersonRepository(dataContext));

        // no caching, every time new repo is created on access
        public IContactRepository Contacts =>
            GetOrCreateRepository(dataContext => new ContactRepository(dataContext));

        public IContactTypeRepository ContactTypes =>
            GetOrCreateRepository(dataContext => new ContactTypeRepository(dataContext));

        public IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>() where TEntity : class, IBaseEntity, new() =>
            GetOrCreateRepository((dataContext) => new BaseRepository<TEntity>(dataContext));


        public AppUnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        private TRepository GetOrCreateRepository<TRepository>(Func<AppDbContext, TRepository> factoryMethod)
        {
            // try to get repo by type from cache dictionary
            _repositoryCache.TryGetValue(typeof(TRepository), out var repoObject);
            if (repoObject != null)
            {
                // we have it, cat it to correct type and return
                return (TRepository) repoObject;
            }

            // call the factory method to actually create the repo object
            repoObject = factoryMethod(_appDbContext);
            // add it to cache
            _repositoryCache[typeof(TRepository)] = repoObject;
            return (TRepository) repoObject;
        }

        public virtual int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}