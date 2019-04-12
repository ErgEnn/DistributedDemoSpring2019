using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BLL.Base.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.Base;

namespace BLL.Base.Helpers
{
    public class BaseServiceFactory<TUnitOfWork> : IBaseServiceFactory<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        protected readonly Dictionary<Type, Func<TUnitOfWork, object>> ServiceCreationMethods;

        public BaseServiceFactory() : this(new Dictionary<Type, Func<TUnitOfWork, object>>())
        {
        }

        public BaseServiceFactory(Dictionary<Type, Func<TUnitOfWork, object>> serviceCreationMethods)
        {
            ServiceCreationMethods = serviceCreationMethods;
        }


        public void Add<TService>(Func<TUnitOfWork, TService> creationMethod)
        where TService: class
        {
            ServiceCreationMethods.Add(typeof(TService), creationMethod );
        }

        public virtual Func<TUnitOfWork, object> GetServiceFactory<TService>()
        {
            // try to get repo by type from cache dictionary
            ServiceCreationMethods.TryGetValue(typeof(TService), out var serviceCreationMethod);
            return serviceCreationMethod;
        }

        public virtual Func<TUnitOfWork, object> GetServiceFactoryForEntity<TEntity>()
            where TEntity : class, IBaseEntity, new()
        {
            return uow => new BaseEntityService<TEntity, TUnitOfWork>(uow);
        }
    }
}