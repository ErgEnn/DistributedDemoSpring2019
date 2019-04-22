using System;
using System.Collections.Generic;
using Contracts.BLL.Base.Helpers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace BLL.Base.Helpers
{
    public class BaseServiceProvider<TUnitOfWork> : IBaseServiceProvider
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly Dictionary<Type, object> ServiceCache;
        protected readonly IBaseServiceFactory<TUnitOfWork> ServiceFactory;
        protected readonly TUnitOfWork Uow;


        public BaseServiceProvider(IBaseServiceFactory<TUnitOfWork> serviceFactory, TUnitOfWork uow) : this(
            new Dictionary<Type, object>(), serviceFactory, uow)
        {
        }

        public BaseServiceProvider(Dictionary<Type, object> serviceCache,
            IBaseServiceFactory<TUnitOfWork> serviceFactory,
            TUnitOfWork uow)
        {
            ServiceCache = serviceCache;
            ServiceFactory = serviceFactory;
            Uow = uow;
        }

        public virtual TService GetService<TService>()
        {
            if (ServiceCache.ContainsKey(typeof(TService)))
            {
                return (TService) ServiceCache[typeof(TService)];
            }
            // didn't find the repo in cache, lets create it

            var repoCreationMethod = ServiceFactory.GetServiceFactory<TService>();


            object repo = repoCreationMethod(Uow);


            ServiceCache[typeof(TService)] = repo;
            return (TService) repo;
        }

        public virtual IBaseEntityService<TBLLEntity> GetEntityService<TBLLEntity, TDALEntity, TDomainEntity>()
            where TBLLEntity : class, new()
            where TDALEntity : class, new()
            where TDomainEntity : class, IBaseEntity, new()        
        {
            if (ServiceCache.ContainsKey(typeof(IBaseEntityService<TBLLEntity>)))
            {
                return (IBaseEntityService<TBLLEntity>) ServiceCache[typeof(IBaseEntityService<TBLLEntity>)];
            }

            // didn't find the repo in cache, lets create it
            var repoCreationMethod = ServiceFactory.GetEntityServiceFactory<TBLLEntity, TDALEntity, TDomainEntity>();

            object repo = repoCreationMethod(Uow);


            ServiceCache[typeof(IBaseEntityService<TBLLEntity>)] = repo;
            return (IBaseEntityService<TBLLEntity>) repo;
        }
    }
}