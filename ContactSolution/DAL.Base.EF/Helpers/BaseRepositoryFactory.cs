using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.Base.EF.Helpers
{
    public class BaseRepositoryFactory : IBaseRepositoryFactory
    {
        protected readonly Dictionary<Type, Func<IDataContext, object>> _repositoryCreationMethodCache;

        public BaseRepositoryFactory() : this(new Dictionary<Type, Func<IDataContext, object>>())
        {
            
        }
        public BaseRepositoryFactory(Dictionary<Type, Func<IDataContext, object>> repositoryCreationMethods)
        {
            _repositoryCreationMethodCache = repositoryCreationMethods;
        }

        public Func<IDataContext, object> GetRepositoryFactory<TRepository>(){
            if (_repositoryCreationMethodCache.ContainsKey(typeof(TRepository)))
            {
                return  _repositoryCreationMethodCache[typeof(TRepository)];
            }

            throw new NullReferenceException("No repo creation method found for " + typeof(TRepository).FullName);
        }
        
        public  Func<IDataContext, object> GetEntityRepositoryFactory<TEntity>() where TEntity : class, IBaseEntity<int>, new()
        {
            return (IDataContext dataContext) => new BaseRepositoryAsync<TEntity>(dataContext);
        }
    }
}