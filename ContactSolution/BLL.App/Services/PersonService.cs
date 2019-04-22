using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Base.Helpers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO; 

namespace BLL.App.Services
{
    public class PersonService : BaseEntityService<BLLAppDTO.Person, DALAppDTO.Person, Domain.Person, IAppUnitOfWork>, IPersonService
    {
        public PersonService(IAppUnitOfWork uow) : base(new BaseBLLMapper<BLLAppDTO.Person, DALAppDTO.Person>(),  uow)
        {
        }

        public async Task<List<BLLAppDTO.Person>> AllForUserAsync(int userId)
        {
            return (await Uow.Persons.AllForUserAsync(userId)).Select(p => BaseBLLMapper.Map<BLLAppDTO.Person>(p)).ToList();
        }

        public async Task<BLLAppDTO.Person> FindForUserAsync(int id, int userId)
        {
            return BaseBLLMapper.Map<BLLAppDTO.Person>(await Uow.Persons.FindForUserAsync(id, userId));
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.Persons.BelongsToUserAsync(id, userId);
        }
    }
}