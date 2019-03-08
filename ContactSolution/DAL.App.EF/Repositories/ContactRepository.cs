using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactRepository: BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }    
}