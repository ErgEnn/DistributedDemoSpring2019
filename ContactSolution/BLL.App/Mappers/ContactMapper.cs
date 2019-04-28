using System;
using Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO;
using externalDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ContactMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Contact))
            {
                return MapFromInternal((internalDTO.Contact) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Contact))
            {
                return MapFromExternal((externalDTO.Contact) inObject) as TOutObject;
            }
            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Contact MapFromInternal(internalDTO.Contact contact)
        {
            var res = contact == null ? null : new externalDTO.Contact
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

        public static internalDTO.Contact MapFromExternal(externalDTO.Contact contact)
        {
            var res = contact == null ? null : new internalDTO.Contact
            {
                Id = contact.Id,
                PersonId = contact.PersonId,
                Person = PersonMapper.MapFromBLL(contact.Person),
                ContactTypeId = contact.ContactTypeId,
                ContactType = ContactTypeMapper.MapFromBLL(contact.ContactType),
                ContactValue = contact.ContactValue
            };
            return res;
        }
    }
}