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

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.Contact MapFromDomain(Domain.Contact contact)
        {
            var res = contact == null ? null : new DAL.App.DTO.Contact
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

        public static Domain.Contact MapFromDAL(DAL.App.DTO.Contact contact)
        {
            var res = contact == null ? null : new Domain.Contact
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