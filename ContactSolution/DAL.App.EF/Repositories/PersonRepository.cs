using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository:BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Person>> AllAsync(int userId)
        {
            return await RepositoryDbSet
                .Include(a => a.AppUser)
                .Where(b => b.AppUserId == userId)
                .ToListAsync();
        }

        public override async Task<Person> FindAsync(params object[] id)
        {
            var person = await base.FindAsync(id);

            if (person != null)
            {
                await RepositoryDbContext.Entry(person).Reference(c => c.AppUser).LoadAsync();
            }
            
            return person;
        }
        
        
    }
}