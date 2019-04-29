using System;
using externalDTO = PublicApi.v2.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v2.Mappers
{
    public class ContactMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Contact))
            {
                // map internal to external
                return MapFromBLL((internalDTO.Contact) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Contact))
            {
                // map from external to internal
                return MapFromExternal((externalDTO.Contact) inObject) as TOutObject;
            }
            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Contact MapFromBLL(internalDTO.Contact contact)
        {
            var res = contact == null ? null : new externalDTO.Contact
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

        public static internalDTO.Contact MapFromExternal(externalDTO.Contact contact)
        {
            var res = contact == null ? null : new internalDTO.Contact
            {
                Id = contact.Id,
                PersonId = contact.PersonId,
                Person = PersonMapper.MapFromExternal(contact.Person),
                ContactTypeId = contact.ContactTypeId,
                ContactType = ContactTypeMapper.MapFromExternal(contact.ContactType),
                ContactValue = contact.ContactValue
            };
            return res;
        }
    }
}