using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.akaver.BLL.Base.Services;

namespace BLL.App.Services
{
    public class PersonService : BaseEntityService<BLL.App.DTO.Person, DAL.App.DTO.Person, IAppUnitOfWork>, IPersonService
    {
        public PersonService(IAppUnitOfWork uow) : base(uow, new PersonMapper())
        {
            ServiceRepository = Uow.Persons;  //Uow.BaseRepository<DAL.App.DTO.Person, Domain.Person>();
        }

        public async Task<List<BLL.App.DTO.Person>> AllForUserAsync(int userId)
        {
            return (await Uow.Persons.AllForUserAsync(userId)).Select(e => PersonMapper.MapFromDAL(e)).ToList();
        }

        public async Task<BLL.App.DTO.Person> FindForUserAsync(int id, int userId)
        {
            return PersonMapper.MapFromDAL( await Uow.Persons.FindForUserAsync(id, userId));
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.Persons.BelongsToUserAsync(id, userId);
        }
    }
}