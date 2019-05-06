using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.akaver.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using ContactType = DAL.App.DTO.ContactType;

namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : BaseRepository<DAL.App.DTO.ContactType,  Domain.ContactType, AppDbContext>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ContactTypeMapper())
        {
        }


        public async override Task<ContactType> FindAsync(params object[] id)
        {
            var culture = Thread.CurrentThread.CurrentUICulture.Name.Substring(0, 2).ToLower();
            
            var contactType = await RepositoryDbSet.FindAsync(id);
            if (contactType != null)
            {
                await RepositoryDbContext.Entry(contactType)
                    .Reference(c => c.ContactTypeValue)
                    .LoadAsync();
                await RepositoryDbContext.Entry(contactType.ContactTypeValue)
                    .Collection(b => b.Translations)
                    .Query()
                    .Where(t => t.Culture == culture)
                    .LoadAsync();
            }
 
            return ContactTypeMapper.MapFromDomain(contactType);
        }

        public override ContactType Update(ContactType entity)
        {
            var entityInDb = RepositoryDbSet
                .Include(m => m.ContactTypeValue)
                .ThenInclude(t => t.Translations)
                .FirstOrDefault(x => x.Id == entity.Id);
            
            entityInDb.ContactTypeValue.SetTranslation(entity.ContactTypeValue);

            return entity;
        }
        
        
        /*
       public virtual TDALEntity Update(TDALEntity entity)
        {
            return _mapper.Map<TDALEntity>(RepositoryDbSet.Update(_mapper.Map<TDomainEntity>(entity)).Entity);
        }
         */
        
        /// <summary>
        /// Get all the records, include contacts
        /// </summary>
        /// <returns></returns>
        public override async Task<List<DAL.App.DTO.ContactType>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(m => m.ContactTypeValue)
                .ThenInclude(t => t.Translations)
                .Include(c => c.Contacts)
                .Select(e => ContactTypeMapper.MapFromDomain(e)).ToListAsync();
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

            var culture = Thread.CurrentThread.CurrentUICulture.Name.Substring(0, 2).ToLower();
        
             var res = await RepositoryDbSet
                .Include(m => m.ContactTypeValue)
                .ThenInclude(t => t.Translations)
                //.Where(x => x.ContactTypeValue.Translations.Any(t => t.Culture == culture))
                .Select(c => new
                {
                    Id = c.Id,
                    ContactTypeValue = c.ContactTypeValue,
                    Translations = c.ContactTypeValue.Translations,
                    ContactCount = c.Contacts.Count
                })
                .ToListAsync();

             
             var resultList = res.Select(c => new ContactTypeWithContactCounts()
             {
                 Id = c.Id,
                 ContactCount = c.ContactCount,
                 ContactTypeValue = c.ContactTypeValue.Translate()
                     
             }).ToList();
             return resultList;
        }
    }
}