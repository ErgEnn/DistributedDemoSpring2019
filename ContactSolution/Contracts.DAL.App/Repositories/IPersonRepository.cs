using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        // add here custom methods
        Task<IEnumerable<Person>> AllAsync(int userId);
    }
}