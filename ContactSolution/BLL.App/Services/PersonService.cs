using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using Domain;

namespace BLL.App.Services
{
    public class PersonService : BaseEntityService<Person, IAppUnitOfWork>, IPersonService
    {
        public PersonService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<IEnumerable<Person>> AllForUserAsync(int userId)
        {
            return await Uow.Persons.AllForUserAsync(userId);
        }
        
    }
}