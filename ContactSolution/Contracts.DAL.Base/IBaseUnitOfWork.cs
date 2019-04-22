using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        IBaseRepository<TDALEntity> BaseRepository<TDALEntity, TDomainEntity>()
            where TDALEntity : class, new()
            where TDomainEntity : class, IBaseEntity, new();

        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}