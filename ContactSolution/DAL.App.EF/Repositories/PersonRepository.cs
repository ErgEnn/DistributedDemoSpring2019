using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Helpers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : BaseRepository<DALAppDTO.Person, Domain.Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext repositoryDbContext) : base(
            new BaseDALMapper<DALAppDTO.Person, Domain.Person>(),
            repositoryDbContext)
        {
        }

        public async Task<List<DALAppDTO.Person>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet.Where(p => p.AppUserId == userId)
                .Select(p => BaseDALMapper.Map<DALAppDTO.Person>(p)).ToListAsync();
        }

        public async Task<DALAppDTO.Person> FindForUserAsync(int id, int userId)
        {
            return BaseDALMapper.Map<DALAppDTO.Person>(await RepositoryDbSet.Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id && p.AppUserId == userId));
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId);
        }
    }
}