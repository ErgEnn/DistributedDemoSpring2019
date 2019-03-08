using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        private readonly IRepositoryProvider _repositoryProvider;

        public IPersonRepository Persons =>
            _repositoryProvider.GetRepository<IPersonRepository>();

        // no caching, every time new repo is created on access
        public IContactRepository Contacts =>
            _repositoryProvider.GetRepository<IContactRepository>();

        public IContactTypeRepository ContactTypes =>
            _repositoryProvider.GetRepository<IContactTypeRepository>();

        public IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>() where TEntity : class, IBaseEntity, new() =>
            _repositoryProvider.GetRepositoryForEntity<TEntity>();


        public AppUnitOfWork(AppDbContext appDbContext, IRepositoryProvider repositoryProvider)
        {
            _appDbContext = appDbContext;
            _repositoryProvider = repositoryProvider;
        }




        public virtual int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}