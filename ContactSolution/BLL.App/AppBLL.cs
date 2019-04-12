using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.App;
using Contracts.DAL.Base;

namespace BLL.App
{
    public class AppBLL :  BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow, IBaseServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
        }

        public IPersonService Persons => ServiceProvider.GetService<IPersonService>();
        public IContactService Contacts  => ServiceProvider.GetService<IContactService>();
        public IContactTypeService ContactTypes  => ServiceProvider.GetService<IContactTypeService>();
    }
}