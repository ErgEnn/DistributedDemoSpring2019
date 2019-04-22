using System;
using System.Threading.Tasks;
using Contracts.Base;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace Contracts.BLL.Base
{
    public interface IBaseBLL : ITrackableInstance
    {
        IBaseEntityService<TBLLEntity> BaseEntityService<TBLLEntity, TDALEntity, TDomainEntity>()
            where TBLLEntity : class, new()
            where TDALEntity : class, new()
            where TDomainEntity : class, IBaseEntity, new();    
        
        Task<int> SaveChangesAsync();   
    }
}