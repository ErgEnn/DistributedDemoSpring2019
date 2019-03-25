using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepository : IBaseRepositoryAsync<Contact, int>
    {
    }
    
}