using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IPersonService Persons { get; }
        IContactService Contacts { get; }
        IContactTypeService ContactTypes { get; }
    }
}