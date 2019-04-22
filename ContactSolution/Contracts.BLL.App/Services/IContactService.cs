using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO; 
namespace Contracts.BLL.App.Services
{
    public interface IContactService : IBaseEntityService<BLLAppDTO.Contact>
    {
        Task<List<BLLAppDTO.Contact>> AllForUserAsync(int userId);
        Task<BLLAppDTO.Contact> FindForUserAsync(int id, int userId);
        Task<bool> BelongsToUserAsync(int id, int userId);
    }
}