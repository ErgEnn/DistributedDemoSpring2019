using System;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceFactory<TUnitOfWork>
        where TUnitOfWork : IBaseUnitOfWork
    {
        void AddToCreationMethods<TService>(Func<TUnitOfWork, TService> creationMethod)
            where TService : class;

        Func<TUnitOfWork, object> GetServiceFactory<TService>();

        
        Func<TUnitOfWork, object> GetEntityServiceFactory<TBLLEntity, TDALEntity, TDomainEntity>()
            where TBLLEntity : class, new()
            where TDALEntity : class, new()
            where TDomainEntity : class, IBaseEntity, new();
    }
}