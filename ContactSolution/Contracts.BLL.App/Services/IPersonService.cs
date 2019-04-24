using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IPersonService : IBaseEntityService<BLLAppDTO.Person>, IPersonRepository<BLLAppDTO.Person>
    {
    }
}