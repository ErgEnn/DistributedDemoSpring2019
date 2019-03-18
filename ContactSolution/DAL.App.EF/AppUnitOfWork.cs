using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Remotion.Linq.Clauses;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork, IAppUnitOfWork
    {
        
        public AppUnitOfWork(AppDbContext dbContext) : base(dbContext)
        {
        }

        private IContactRepository _contactRepository;        
        public IContactRepository Contacts => 
            _contactRepository ?? (_contactRepository = new ContactRepository((AppDbContext) UOWDbContext));

        public IContactTypeRepository ContactTypes =>
            GetOrCreateRepository<IContactTypeRepository>((ctx) => new ContactTypeRepository(ctx));
        
        public IPersonRepository Persons => 
            GetOrCreateRepository<IPersonRepository>((ctx) => new PersonRepository(ctx));


        // repo factory
        private readonly Dictionary<Type, object> _repositoryCache  = new Dictionary<Type, object>();
        private TRepository GetOrCreateRepository<TRepository>(Func<AppDbContext, TRepository> repoCreationMethod)  
        {

            if (_repositoryCache.ContainsKey(typeof(TRepository)))
            {
                return (TRepository) _repositoryCache[typeof(TRepository)];
            }

            // we didnt find the correct repo, create it

            object repo = repoCreationMethod((AppDbContext) UOWDbContext);
        

            _repositoryCache[typeof(TRepository)] = repo;
            return (TRepository) repo;

        }
        
    }
}