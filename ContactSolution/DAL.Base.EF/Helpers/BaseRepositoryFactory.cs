using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using DAL.Base.EF.Repositories;

namespace DAL.Base.EF.Helpers
{
    public class BaseRepositoryFactory : IRepositoryFactory
    {
        protected readonly Dictionary<Type, Func<IDataContext, object>> RepositoryCreationMethods;

        public BaseRepositoryFactory() : this(new Dictionary<Type, Func<IDataContext, object>>())
        {
        }
        
        public BaseRepositoryFactory(Dictionary<Type, Func<IDataContext, object>> repositoryCreationMethods)
        {
            RepositoryCreationMethods = repositoryCreationMethods;
        }
        
        public Func<IDataContext, object> GetRepositoryFactory<TRepo>()
        {
            // try to get repo by type from cache dictionary
            RepositoryCreationMethods.TryGetValue(typeof(TRepo), out var repoCreationMethod);
            return repoCreationMethod;
        }

        public Func<IDataContext, object> GetRepositoryFactoryForEntity<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return dataContext => new BaseRepository<TEntity>(dataContext);
        }
    }
}