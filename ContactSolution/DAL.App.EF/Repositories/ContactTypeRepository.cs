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
    public class ContactTypeRepository : BaseRepository<DALAppDTO.ContactType, Domain.ContactType, AppDbContext>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext repositoryDbContext) : base(new BaseDALMapper<DALAppDTO.ContactType, Domain.ContactType>(), repositoryDbContext)
        {
        }

        /// <summary>
        /// Get all the records, include contacts
        /// </summary>
        /// <returns></returns>
        public override async Task<List<DALAppDTO.ContactType>> AllAsync()
        {
            return await RepositoryDbSet.Include(c => c.Contacts).Select(c => BaseDALMapper.Map<DALAppDTO.ContactType>(c)).ToListAsync();
        }

        /// <summary>
        /// Get all the ContactTypes from db, include count of contacts for every ContactType
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<DALAppDTO.ContactTypeContactCount>> GetAllWithContactCountAsync()
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
                .Select(c => new DALAppDTO.ContactTypeContactCount()
                {
                    Id = c.Id,
                    ContactTypeValue = c.ContactTypeValue,
                    ContactCount = c.Contacts.Count
                })
                .ToListAsync();
        }
    }
}