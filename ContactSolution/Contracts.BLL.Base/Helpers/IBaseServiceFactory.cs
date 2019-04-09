using System;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceFactory
    {
        Func<IBaseUnitOfWork, object> GetServiceFactory<TService>();

        Func<IBaseUnitOfWork, object> GetEntityServiceFactory<TEntity>()
            where TEntity : class, IBaseEntity<int>, new();

    }
}