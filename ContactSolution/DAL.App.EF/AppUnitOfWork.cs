using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
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
        
        // caching is ok, but creation is fixed - repeating code
        private IPersonRepository _personRepository;
        public IPersonRepository Persons => 
            _personRepository ?? (_personRepository = new PersonRepository(_appDbContext));
        
        // no caching, every time new repo is created on access
        public IContactRepository Contacts => new ContactRepository(_appDbContext);
        
        //better, creation is centralized into getorcreate factory
        public IContactTypeRepository ContactTypes => 
            GetOrCreateRepository((dataContext) => new ContactTypeRepository(dataContext));

        
        public AppUnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class, new()
        {
            return new BaseRepository<TEntity>(_appDbContext);
        }


        private TRepository GetOrCreateRepository<TRepository>(Func<AppDbContext,TRepository> factoryMethod)
        {
            // try to get repo by type from cache dictionary
            _repositoryCache.TryGetValue(typeof(TRepository), out var repoObject);
            if (repoObject != null)
            {
                // we have it, cat it to correct type and return
                return (TRepository) repoObject;
            }

            repoObject = factoryMethod(_appDbContext);
            _repositoryCache[typeof(TRepository)] = repoObject;
            return (TRepository) repoObject;
        }

        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}