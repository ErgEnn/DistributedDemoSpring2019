using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.Base.EF.Helpers
{
    public class BaseRepositoryProvider : IBaseRepositoryProvider
    {
        protected readonly Dictionary<Type, object> _repositoryCache;
        protected readonly IBaseRepositoryFactory _repositoryFactory;
        protected readonly IDataContext _dataContext;
        
        public BaseRepositoryProvider(IBaseRepositoryFactory repositoryFactory, IDataContext dataContext):
            this(new Dictionary<Type, object>(), repositoryFactory, dataContext)
        {
        }
        
        public BaseRepositoryProvider(Dictionary<Type, object> repositoryCache, IBaseRepositoryFactory repositoryFactory, IDataContext dataContext)
        {
            _repositoryCache = repositoryCache;
            _repositoryFactory = repositoryFactory;
            _dataContext = dataContext;
        }

        public virtual TRepository GetRepository<TRepository>()
        {
            if (_repositoryCache.ContainsKey(typeof(TRepository)))
            {
                return (TRepository) _repositoryCache[typeof(TRepository)];
            }
            // didn't find the repo in cache, lets create it

            var repoCreationMethod = _repositoryFactory.GetRepositoryFactory<TRepository>();


            object repo = repoCreationMethod(_dataContext);
        

            _repositoryCache[typeof(TRepository)] = repo;
            return (TRepository) repo;
        }


        public virtual IBaseRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            if (_repositoryCache.ContainsKey(typeof(IBaseRepositoryAsync<TEntity>)))
            {
                return (IBaseRepository<TEntity>) _repositoryCache[typeof(IBaseRepositoryAsync<TEntity>)];
            }
            // didn't find the repo in cache, lets create it
            var repoCreationMethod = _repositoryFactory.GetEntityRepositoryFactory<TEntity>();

            object repo = repoCreationMethod(_dataContext);


            _repositoryCache[typeof(IBaseRepositoryAsync<TEntity>)] = repo;
            return (IBaseRepository<TEntity>) repo;

        }


    }
}