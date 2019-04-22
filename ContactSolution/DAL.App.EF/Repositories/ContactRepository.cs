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
    public class ContactRepository : BaseRepository<DAL.App.DTO.Contact, Domain.Contact, AppDbContext>, IContactRepository
    {
        public ContactRepository(AppDbContext repositoryDbContext) : base(new BaseDALMapper<DALAppDTO.Contact,Domain.Contact>(), repositoryDbContext)
        {
        }

        public async Task<List<DALAppDTO.Contact>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(c => c.Person.AppUserId == userId).Select(c => BaseDALMapper.Map<DALAppDTO.Contact>(c)).ToListAsync();
        }

        public async Task<DALAppDTO.Contact> FindForUserAsync(int id, int userId)
        {
            var contact = await RepositoryDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id && m.Person.AppUserId == userId);

            return BaseDALMapper.Map<DALAppDTO.Contact>(contact);
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet.AnyAsync(c => c.Id == id && c.Person.AppUserId == userId);
        }
    }
}