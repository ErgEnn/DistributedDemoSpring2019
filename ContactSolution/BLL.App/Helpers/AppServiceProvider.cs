using System;
using System.Collections.Generic;
using BLL.Base.Helpers;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.App;
using Contracts.DAL.Base;

namespace BLL.App.Helpers
{
    public class AppServiceProvider : BaseServiceProvider
    {
        protected readonly IAppUnitOfWork AppUnitOfWork;
        
        public AppServiceProvider(IBaseServiceFactory serviceFactory, IAppUnitOfWork uow) : this(new Dictionary<Type, object>(), serviceFactory, uow)
        {
        }

        public AppServiceProvider(Dictionary<Type, object> serviceCache, IBaseServiceFactory serviceFactory, IAppUnitOfWork baseUnitOfWork) : base(serviceCache, serviceFactory, baseUnitOfWork)
        {
            AppUnitOfWork = baseUnitOfWork;
        }
        
        
        public override TService GetService<TService>()
        {
            if (ServiceCache.ContainsKey(typeof(TService)))
            {
                return (TService) ServiceCache[typeof(TService)];
            }
            // didn't find the repo in cache, lets create it

            var repoCreationMethod = ServiceFactory.GetServiceFactory<TService>();


            object repo = repoCreationMethod(AppUnitOfWork);


            ServiceCache[typeof(TService)] = repo;
            return (TService) repo;
        }
        
        
    }
}