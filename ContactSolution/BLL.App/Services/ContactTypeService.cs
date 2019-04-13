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
    public class ContactTypeService : BaseEntityService<ContactType, IAppUnitOfWork>, IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<IEnumerable<ContactTypeDTO>> GetAllWithContactCountAsync()
        {
            return await Uow.ContactTypes.GetAllWithContactCountAsync();
        }
    }
}