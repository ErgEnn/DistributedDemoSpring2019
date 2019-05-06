using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.akaver.Contracts.DAL.Base.Helpers;
using ee.itcollege.akaver.DAL.Base.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Remotion.Linq.Clauses;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        
        public AppUnitOfWork(AppDbContext dbContext, IBaseRepositoryProvider repositoryProvider) : base(dbContext, repositoryProvider)
        {
        }

        public IContactRepository Contacts =>
            _repositoryProvider.GetRepository<IContactRepository>();

        public IContactTypeRepository ContactTypes =>
            _repositoryProvider.GetRepository<IContactTypeRepository>();

        
        public IPersonRepository Persons => 
            _repositoryProvider.GetRepository<IPersonRepository>();
 
        
    }
}