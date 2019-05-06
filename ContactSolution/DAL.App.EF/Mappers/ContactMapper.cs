using System;
using ee.itcollege.akaver.Contracts.DAL.Base.Mappers;
using internalDTO = Domain;
using externalDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ContactMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Contact))
            {
                return MapFromDomain((internalDTO.Contact) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Contact))
            {
                return MapFromDAL((externalDTO.Contact) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Contact MapFromDomain(internalDTO.Contact contact)
        {
            var res = contact == null ? null : new externalDTO.Contact
            {
                Id = contact.Id,
                PersonId = contact.PersonId,
                Person = PersonMapper.MapFromDomain(contact.Person),
                ContactTypeId = contact.ContactTypeId,
                ContactType = ContactTypeMapper.MapFromDomain(contact.ContactType),
                ContactValue = contact.ContactValue
            };


            return res;
        }

        public static internalDTO.Contact MapFromDAL(externalDTO.Contact contact)
        {
            var res = contact == null ? null : new internalDTO.Contact
            {
                Id = contact.Id,
                PersonId = contact.PersonId,
                Person = PersonMapper.MapFromDAL(contact.Person),
                ContactTypeId = contact.ContactTypeId,
                ContactType = ContactTypeMapper.MapFromDAL(contact.ContactType),
                ContactValue = contact.ContactValue
            };


            return res;
        }
    }
}