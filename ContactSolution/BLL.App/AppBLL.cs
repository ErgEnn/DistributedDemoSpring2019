using System;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.App;
using ee.itcollege.akaver.BLL.Base;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected readonly IAppUnitOfWork AppUnitOfWork;
        
        public AppBLL(IAppUnitOfWork appUnitOfWork, IBaseServiceProvider serviceProvider) : base(appUnitOfWork, serviceProvider)
        {
            AppUnitOfWork = appUnitOfWork;
        }

        public IContactService Contacts => ServiceProvider.GetService<IContactService>();
        public IContactTypeService ContactTypes => ServiceProvider.GetService<IContactTypeService>();
        public IPersonService Persons => ServiceProvider.GetService<IPersonService>();
        
    }
}