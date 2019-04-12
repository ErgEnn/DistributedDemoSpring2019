using System;
using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Helpers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork: IUnitOfWork
    {
        protected readonly TUnitOfWork UOW;
        protected readonly IBaseServiceProvider ServiceProvider;
        
        public BaseBLL(TUnitOfWork uow, IBaseServiceProvider serviceProvider)
        {
            UOW = uow;
            ServiceProvider = serviceProvider;
        }
        
        public int SaveChanges()
        {
            return UOW.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOW.SaveChangesAsync();
        }

        public IBaseEntityService<TEntity> BaseService<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return ServiceProvider.GetServiceForEntity<TEntity>();
        }
    }
}