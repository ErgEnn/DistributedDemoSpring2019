using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;

namespace DAL.Base.EF.Helpers
{
    public class BaseRepositoryProvider : IRepositoryProvider
    {
        // repo cache
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IDataContext _dataContext;

        public BaseRepositoryProvider(IRepositoryFactory repositoryFactory, IDataContext dataContext)
        {
            _repositoryFactory = repositoryFactory;
            _dataContext = dataContext;
        }

        public TRepository GetRepository<TRepository>()
        {
            // try to get repo by type from cache dictionary
            _repositoryCache.TryGetValue(typeof(TRepository), out var repoObject);
            if (repoObject != null)
            {
                // we have it, cat it to correct type and return
                return (TRepository) repoObject;
            }

            // repo was not found in cache, create it

            // get the creation method from factory
            var repoCreationMethod = _repositoryFactory.GetRepositoryFactory<TRepository>();
            if (repoCreationMethod == null)
            {
                throw new NullReferenceException("No factory found for repository " + typeof(TRepository).Name);
            }

            repoObject = repoCreationMethod(_dataContext);

            // add repo to cache
            _repositoryCache[typeof(TRepository)] = repoObject;
            
            // and return repo
            return (TRepository) repoObject;
        }
        
 


        public IBaseRepositoryAsync<TEntity> GetRepositoryForEntity<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            // try to get repo by type from cache dictionary
            _repositoryCache.TryGetValue(typeof(IBaseRepositoryAsync<TEntity>), out var repoObject);
            if (repoObject != null)
            {
                // we have it, cat it to correct type and return
                return (IBaseRepositoryAsync<TEntity>) repoObject;
            }
            
            
            // get the creation method from factory
            var repoCreationMethod = _repositoryFactory.GetRepositoryFactoryForEntity<TEntity>();
            if (repoCreationMethod == null)
            {
                throw new NullReferenceException("No factory found for entity based repository! Entity: " + typeof(TEntity).Name);
            }

            repoObject = repoCreationMethod(_dataContext);

            // add repo to cache
            _repositoryCache[typeof(IBaseRepositoryAsync<TEntity>)] = repoObject;
            
            // and return repo
            return (IBaseRepositoryAsync<TEntity>) repoObject;
        }
    }
}