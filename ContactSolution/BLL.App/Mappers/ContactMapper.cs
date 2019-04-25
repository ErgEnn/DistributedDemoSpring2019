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
            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.Contact MapFromDAL(DAL.App.DTO.Contact contact)
        {
            var res = contact == null ? null : new BLL.App.DTO.Contact
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

        public static DAL.App.DTO.Contact MapFromBLL(BLL.App.DTO.Contact contact)
        {
            var res = contact == null ? null : new DAL.App.DTO.Contact
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