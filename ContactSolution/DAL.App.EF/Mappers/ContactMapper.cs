using System;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ContactMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.Contact))
            {
                return MapFromDomain((Domain.Contact) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Contact))
            {
                return MapFromDAL((DAL.App.DTO.Contact) inObject) as TOutObject;
            }

            throw new InvalidCastException("No conversion");
        }

        public static DAL.App.DTO.Contact MapFromDomain(Domain.Contact contact)
        {
            var res = new DAL.App.DTO.Contact();
            res.Id = contact.Id;

            res.PersonId = contact.PersonId;
            // TODO: Fix this, use DAL.App.DTO.Person inside DTO.Contact
            res.Person = new Person()
            {
                Id = contact.Person.Id,
                AppUserId = contact.Person.AppUserId,
                FirstName = contact.Person.FirstName,
                LastName = contact.Person.LastName,
            };

            res.ContactTypeId = contact.ContactTypeId;
            res.ContactType = new ContactType()
            {
                Id = contact.ContactType.Id,
                ContactTypeValue = contact.ContactType.ContactTypeValue
            };


            res.ContactValue = contact.ContactValue;

            return res;
        }

        public static Domain.Contact MapFromDAL(DAL.App.DTO.Contact contact)
        {
            var res = new Domain.Contact();
            throw new NotImplementedException();
            
            return res;
        }
    }
}