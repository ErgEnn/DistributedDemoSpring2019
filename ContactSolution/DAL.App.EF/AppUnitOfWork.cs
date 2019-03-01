using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public IPersonRepository Persons => new PersonRepository(_appDbContext);
        public IContactRepository Contacts => new ContactRepository(_appDbContext);
        public IContactTypeRepository ContactTypes => new ContactTypeRepository(_appDbContext);

        
        public AppUnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class, new()
        {
            return new BaseRepository<TEntity>(_appDbContext);
        }

        
        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}