using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : BaseRepositoryAsync<Person>, IPersonRepository
    {
        public PersonRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }
    }
}