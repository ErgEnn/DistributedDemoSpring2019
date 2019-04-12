using System;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceFactory<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        Func<TUnitOfWork, object> GetServiceFactory<TRepo>();

        Func<TUnitOfWork, object> GetServiceFactoryForEntity<TEntity>()
            where TEntity : class, IBaseEntity, new();
    }
}