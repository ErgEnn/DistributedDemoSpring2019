using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IContactService Contacts { get; }
        IContactTypeService ContactTypes { get; }
        IPersonService Persons { get; }
        // TODO: Public facing services
        // IContactBookService ContactBook
    }
}