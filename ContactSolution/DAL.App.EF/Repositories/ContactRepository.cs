using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Helpers;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactRepository : BaseRepository<DAL.App.DTO.Contact, Domain.Contact, AppDbContext>,
        IContactRepository
    {

        public ContactRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new ContactMapper())
        {
        }


        public async Task<List<DAL.App.DTO.Contact>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(c => c.Person.AppUserId == userId)
                .Select(e => ContactMapper.MapFromDomain(e)).ToListAsync();
        }

        public async Task<DAL.App.DTO.Contact> FindForUserAsync(int id, int userId)
        {
            var contact = await RepositoryDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id && m.Person.AppUserId == userId);

            return ContactMapper.MapFromDomain(contact) ;
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet
                .AnyAsync(c => c.Id == id && c.Person.AppUserId == userId);
        }
    }
}