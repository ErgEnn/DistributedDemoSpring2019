using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactRepository : BaseRepositoryAsync<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }

    }
}