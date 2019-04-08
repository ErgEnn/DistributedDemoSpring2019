using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected readonly DbContext UOWDbContext;
        protected readonly IBaseRepositoryProvider _repositoryProvider;

        public BaseUnitOfWork(IDataContext dataContext, IBaseRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
            UOWDbContext = (DbContext) dataContext;
        }

        public IBaseRepositoryAsync<TEntity> BaseRepositoryAsync<TEntity>() where TEntity : class, IBaseEntity<int>, new()
        {
            return _repositoryProvider.GetEntityRepository<TEntity>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOWDbContext.SaveChangesAsync();
        }
    }
}