using System;
using externalDTO = PublicApi.v2.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v2.Mappers
{
    public class ContactTypeMapper 
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ContactType))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ContactType) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ContactType))
            {
                // map external to internal
                return MapFromExternal((externalDTO.ContactType) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ContactType MapFromBLL(internalDTO.ContactType contactType)
        {
            var res = contactType == null ? null : new externalDTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        
        public static internalDTO.ContactType MapFromExternal(externalDTO.ContactType contactType)
        {
            var res = contactType == null ? null : new internalDTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        
        
        public static externalDTO.ContactTypeWithContactCounts MapFromBLL(internalDTO.ContactTypeWithContactCounts contactTypeWithContactCounts)
        {
            var res = contactTypeWithContactCounts == null ? null : new externalDTO.ContactTypeWithContactCounts()
            {
                Id = contactTypeWithContactCounts.Id,
                ContactTypeValue = contactTypeWithContactCounts.ContactTypeValue,
                ContactCount = contactTypeWithContactCounts.ContactCount + 100
            };
            return res;
        }

    }
}