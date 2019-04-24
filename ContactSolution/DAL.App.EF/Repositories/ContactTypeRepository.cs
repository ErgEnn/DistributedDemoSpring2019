using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : BaseRepository<DAL.App.DTO.ContactType,  Domain.ContactType, AppDbContext>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ContactTypeMapper())
        {
        }

        /// <summary>
        /// Get all the records, include contacts
        /// </summary>
        /// <returns></returns>
        public override async Task<List<DAL.App.DTO.ContactType>> AllAsync()
        {
            return await RepositoryDbSet.Include(c => c.Contacts).Select(e => ContactTypeMapper.MapFromDomain(e)).ToListAsync();
        }

        /// <summary>
        /// Get all the ContactTypes from db, include count of contacts for every ContactType
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<ContactTypeWithContactCounts>> GetAllWithContactCountAsync()
        {
/*
      Result is single query against db!
      SELECT `c`.`Id`, `c`.`ContactTypeValue`, (
          SELECT COUNT(*)
          FROM `Contacts` AS `c0`
          WHERE `c`.`Id` = `c0`.`ContactTypeId`
      ) AS `ContactCount`
      FROM `ContactTypes` AS `c`

 */
            return await RepositoryDbSet
                .Select(c => new ContactTypeWithContactCounts()
                {
                    Id = c.Id,
                    ContactTypeValue = c.ContactTypeValue,
                    ContactCount = c.Contacts.Count
                })
                .ToListAsync();
        }
    }
}