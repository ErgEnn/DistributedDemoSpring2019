using System;
using System.Collections.Generic;
using BLL.Base.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.Base;

namespace BLL.Base.Helpers
{
    public class BaseServiceFactory : IBaseServiceFactory
    {
        protected readonly Dictionary<Type, Func<IBaseUnitOfWork, object>> ServiceCreationMethodCache;

        public BaseServiceFactory(): this(new Dictionary<Type, Func<IBaseUnitOfWork, object>>())
        {
            
        }
        public BaseServiceFactory(Dictionary<Type, Func<IBaseUnitOfWork, object>> serviceCreationMethodCache)
        {
            ServiceCreationMethodCache = serviceCreationMethodCache;
        }


        public virtual Func<IBaseUnitOfWork, object> GetServiceFactory<TService>()
        {
            if (ServiceCreationMethodCache.ContainsKey(typeof(TService)))
            {
                return  ServiceCreationMethodCache[typeof(TService)];
            }

            throw new NullReferenceException("No service creation method found for " + typeof(TService).FullName);
        }

        public virtual Func<IBaseUnitOfWork, object> GetEntityServiceFactory<TEntity>() where TEntity : class, IBaseEntity<int>, new()
        {
            return (IBaseUnitOfWork uow) => new BaseEntityService<TEntity>(uow);
        }
    }
}