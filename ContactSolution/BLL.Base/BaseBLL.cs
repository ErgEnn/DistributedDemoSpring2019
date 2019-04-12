using System;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork: IUnitOfWork
    {
        protected readonly TUnitOfWork UOW;
        
        public BaseBLL(TUnitOfWork uow)
        {
            UOW = uow;
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
            return new BaseEntityService<TEntity, TUnitOfWork>(UOW);
        }
    }
}