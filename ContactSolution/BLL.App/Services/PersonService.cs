using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class PersonService : BaseEntityService<Person, IAppUnitOfWork>, IPersonService
    {
        public PersonService(IAppUnitOfWork uow) : base(uow)
        {
            
        }

        public async Task<IEnumerable<Person>> AllAsync(int userId)
        {
            return await UOW.Persons.AllAsync(userId);
        }
    }
}