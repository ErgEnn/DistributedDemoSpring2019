using System;
using Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ContactMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.Contact))
            {
                return MapFromDAL((DAL.App.DTO.Contact) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.Contact))
            {
                return MapFromBLL((BLL.App.DTO.Contact) inObject) as TOutObject;
            }

            throw new InvalidCastException("No conversion");
        }

        public static BLL.App.DTO.Contact MapFromDAL(DAL.App.DTO.Contact contact)
        {
            var res = new BLL.App.DTO.Contact
            {
                Id = contact.Id,
                PersonId = contact.PersonId,
                Person = contact.Person == null ? null : new BLL.App.DTO.Person()
                {
                    Id = contact.Person.Id,
                    AppUserId = contact.Person.AppUserId,
                    FirstName = contact.Person.FirstName,
                    LastName = contact.Person.LastName,
                },
                ContactTypeId = contact.ContactTypeId,
                ContactType = contact.ContactType == null ? null : new BLL.App.DTO.ContactType()
                {
                    Id = contact.ContactType.Id, 
                    ContactTypeValue = contact.ContactType.ContactTypeValue
                },
                ContactValue = contact.ContactValue
            };

            return res;
        }

        public static DAL.App.DTO.Contact MapFromBLL(BLL.App.DTO.Contact contact)
        {
            var res = new DAL.App.DTO.Contact
            {
                Id = contact.Id,
                PersonId = contact.PersonId,
                Person = contact.Person == null ? null : new DAL.App.DTO.Person()
                {
                    Id = contact.Person.Id,
                    AppUserId = contact.Person.AppUserId,
                    FirstName = contact.Person.FirstName,
                    LastName = contact.Person.LastName,
                },
                ContactTypeId = contact.ContactTypeId,
                ContactType = contact.ContactType == null ? null : new DAL.App.DTO.ContactType()
                {
                    Id = contact.ContactType.Id, ContactTypeValue = contact.ContactType.ContactTypeValue
                },
                ContactValue = contact.ContactValue
            };
            return res;
        }
    }
}