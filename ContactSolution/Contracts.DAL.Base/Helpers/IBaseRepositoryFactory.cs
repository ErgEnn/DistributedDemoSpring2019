using System;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base.Helpers
{
    public interface IBaseRepositoryFactory
    {
        void AddToCreationMethods<TRepository>(Func<IDataContext, TRepository> creationMethod)
            where TRepository : class;
        
        Func<IDataContext, object> GetRepositoryFactory<TRepository>();

        Func<IDataContext, object> GetEntityRepositoryFactory<TEntity>()
            where TEntity : class, IBaseEntity, new();
        
        

    }
}