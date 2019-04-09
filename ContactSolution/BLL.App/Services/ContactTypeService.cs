using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using DAL.App.DTO;
using Domain;

namespace BLL.App.Services
{
    public class ContactTypeService : BaseEntityService<ContactType>, IContactTypeService
    {
        protected readonly IAppUnitOfWork AppUnitOfWork;

        public ContactTypeService(IBaseUnitOfWork uow) : base(uow)
        {
            AppUnitOfWork = (IAppUnitOfWork) uow;
        }

        public async Task<IEnumerable<ContactTypeDTO>> GetAllWithContactCountAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}