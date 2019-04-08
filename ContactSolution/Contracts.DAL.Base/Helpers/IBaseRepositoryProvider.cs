using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base.Helpers
{
    public interface IBaseRepositoryProvider
    {
        TRepository GetRepository<TRepository>();
        IBaseRepositoryAsync<TEntity> GetEntityRepository<TEntity>() where TEntity : class, IBaseEntity<int>, new();
    }
}