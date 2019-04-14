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
    public class ContactRepository : BaseRepository<Contact, AppDbContext>, IContactRepository
    {
        public ContactRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }

        public async Task<List<Contact>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(c => c.Person.AppUserId == userId).ToListAsync();
        }

        public async Task<Contact> FindForUserAsync(int id, int userId)
        {
            var contact = await RepositoryDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id && m.Person.AppUserId == userId);

            return contact;
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet.AnyAsync(c => c.Id == id && c.Person.AppUserId == userId);
        }
    }
}