using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Remotion.Linq.Clauses;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork, IAppUnitOfWork
    {
        
        public AppUnitOfWork(IDataContext dbContext, IBaseRepositoryProvider repositoryProvider) : base(dbContext, repositoryProvider)
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