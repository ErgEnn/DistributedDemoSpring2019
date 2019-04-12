using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;

namespace BLL.App
{
    public class AppBLL :  BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public IPersonService Persons => new PersonService(UOW);
        public IContactService Contacts  => new ContactService(UOW);
        public IContactTypeService ContactTypes  => new ContactTypeService(UOW);
    }
}