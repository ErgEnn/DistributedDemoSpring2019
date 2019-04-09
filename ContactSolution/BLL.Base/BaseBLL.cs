using System;
using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Helpers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL : IBaseBLL
    {
        public virtual Guid InstanceId { get; } = Guid.NewGuid();


        protected readonly IBaseUnitOfWork BaseUnitOfWork;
        protected readonly IBaseServiceProvider ServiceProvider;

        public BaseBLL(IBaseUnitOfWork baseUnitOfWork, IBaseServiceProvider serviceProvider)
        {
            BaseUnitOfWork = baseUnitOfWork;
            ServiceProvider = serviceProvider;
        }

        public virtual IBaseEntityService<TEntity> BaseEntityService<TEntity>() where TEntity : class, IBaseEntity<int>, new()
        {
            return ServiceProvider.GetEntityService<TEntity>();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await BaseUnitOfWork.SaveChangesAsync();   
        }
        
    }
}