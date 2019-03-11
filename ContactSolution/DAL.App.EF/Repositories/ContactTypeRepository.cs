using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository  : BaseRepositoryAsync<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }
    }
}