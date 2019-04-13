using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<IEnumerable<Contact>> AllForUserAsync(int userId);
        Task<Contact> FindForUserAsync(int id, int userId);
        
    }
    
}