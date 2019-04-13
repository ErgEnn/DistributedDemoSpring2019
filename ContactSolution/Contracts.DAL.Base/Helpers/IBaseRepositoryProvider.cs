using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base.Helpers
{
    public interface IBaseRepositoryProvider
    {
        TRepository GetRepository<TRepository>();
        IBaseRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class, IBaseEntity, new();
    }
}