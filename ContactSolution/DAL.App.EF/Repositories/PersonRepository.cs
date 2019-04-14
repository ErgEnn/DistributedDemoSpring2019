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

        public async Task<List<Person>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet.Where(p => p.AppUserId == userId).ToListAsync();
        }

        public async Task<Person> FindForUserAsync(int id, int userId)
        {
            return await RepositoryDbSet.Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id && p.AppUserId == userId);
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId);
        }
    }
}