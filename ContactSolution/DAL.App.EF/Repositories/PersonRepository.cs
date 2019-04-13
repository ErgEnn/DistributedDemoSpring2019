using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : BaseRepository<Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }

        public async Task<IEnumerable<Person>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet.Where(p => p.AppUserId == userId).ToListAsync();
        }
    }
}