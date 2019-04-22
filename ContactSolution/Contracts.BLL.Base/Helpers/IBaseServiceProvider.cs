using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base.Helpers
{
    public interface IBaseServiceProvider
    {
        TService GetService<TService>();

        IBaseEntityService<TBLLEntity> GetEntityService<TBLLEntity, TDALEntity, TDomainEntity>()
            where TBLLEntity : class, new()
            where TDALEntity : class, new()
            where TDomainEntity : class, IBaseEntity, new();

    }
}