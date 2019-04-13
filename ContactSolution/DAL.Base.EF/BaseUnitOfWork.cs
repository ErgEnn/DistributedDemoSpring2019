using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseUnitOfWork<TDbContext> : IBaseUnitOfWork
    where TDbContext : DbContext
    {
        protected readonly TDbContext UOWDbContext;
        protected readonly IBaseRepositoryProvider _repositoryProvider;

        public BaseUnitOfWork(TDbContext dataContext, IBaseRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
            UOWDbContext =  dataContext;
        }

        public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return _repositoryProvider.GetEntityRepository<TEntity>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOWDbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return UOWDbContext.SaveChanges();
        }
    }
}