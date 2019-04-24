using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : BaseRepository<DAL.App.DTO.Person, Domain.Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new PersonMapper())
        {
        }

        public async Task<List<DAL.App.DTO.Person>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet.Where(p => p.AppUserId == userId).Select(e => PersonMapper.MapFromDomain(e))
                .ToListAsync();
        }

        public async Task<DAL.App.DTO.Person> FindForUserAsync(int id, int userId)
        {
            return PersonMapper.MapFromDomain(await RepositoryDbSet.Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id && p.AppUserId == userId));
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId);
        }
    }
}