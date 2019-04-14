using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> AllForUserAsync(int userId);
        Task<Person> FindForUserAsync(int id, int userId);

        Task<bool> BelongsToUserAsync(int id, int userId);
    }
}