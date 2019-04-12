using System;
using System.Collections.Generic;
using Contracts.BLL.Base.Helpers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;

namespace BLL.Base.Helpers
{
    public class BaseServiceProvider<TUnitOfWork> : IBaseServiceProvider
        where TUnitOfWork : IUnitOfWork
    {
        // repo cache
        private readonly Dictionary<Type, object> _serviceCache = new Dictionary<Type, object>();

        private readonly IBaseServiceFactory<TUnitOfWork> _serviceFactory;
        private readonly TUnitOfWork _uow;

        public BaseServiceProvider(IBaseServiceFactory<TUnitOfWork> serviceFactory, TUnitOfWork uow)
        {
            _serviceFactory = serviceFactory;
            _uow = uow;
        }

        public virtual TService GetService<TService>()
        {
            // try to get repo by type from cache dictionary
            _serviceCache.TryGetValue(typeof(TService), out var serviceObject);
            if (serviceObject != null)
            {
                // we have it, cat it to correct type and return
                return (TService) serviceObject;
            }

            // repo was not found in cache, create it

            // get the creation method from factory
            var repoCreationMethod = _serviceFactory.GetServiceFactory<TService>();
            if (repoCreationMethod == null)
            {
                throw new NullReferenceException("No factory found for service " + typeof(TService).Name);
            }

            serviceObject = repoCreationMethod(_uow);

            // add repo to cache
            _serviceCache[typeof(TService)] = serviceObject;

            // and return repo
            return (TService) serviceObject;
        }


        public virtual IBaseEntityService<TEntity> GetServiceForEntity<TEntity>()
            where TEntity : class, IBaseEntity, new()
        {
            // try to get repo by type from cache dictionary
            _serviceCache.TryGetValue(typeof(IBaseRepositoryAsync<TEntity>), out var serviceObject);
            if (serviceObject != null)
            {
                // we have it, cat it to correct type and return
                return (IBaseEntityService<TEntity>) serviceObject;
            }


            // get the creation method from factory
            var repoCreationMethod = _serviceFactory.GetServiceFactoryForEntity<TEntity>();
            if (repoCreationMethod == null)
            {
                throw new NullReferenceException("No factory found for entity based service! Entity: " +
                                                 typeof(TEntity).Name);
            }

            serviceObject = repoCreationMethod(_uow);

            // add repo to cache
            _serviceCache[typeof(IBaseRepositoryAsync<TEntity>)] = serviceObject;

            // and return repo
            return (IBaseEntityService<TEntity>) serviceObject;
        }
    }
}