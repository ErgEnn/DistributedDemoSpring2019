using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        // all the crud methods
        // do not let IQueriable out of the repo!
        
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();

        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);
        
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        
        TEntity Update(TEntity entity);
        
        void Remove(TEntity entity);
        void Remove(params object[] id);

    }
    
}