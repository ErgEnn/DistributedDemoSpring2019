using System;
using Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO;
using externalDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ContactTypeMapper  : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.ContactType))
            {
                return MapFromDAL((internalDTO.ContactType) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ContactType))
            {
                return MapFromBLL((BLL.App.DTO.ContactType) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static BLL.App.DTO.ContactType MapFromDAL(internalDTO.ContactType contactType)
        {
            var res = contactType == null ? null : new BLL.App.DTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        
        public static internalDTO.ContactType MapFromBLL(BLL.App.DTO.ContactType contactType)
        {
            var res = contactType == null ? null : new internalDTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        
        
        public static BLL.App.DTO.ContactTypeWithContactCounts MapFromDAL(internalDTO.ContactTypeWithContactCounts contactTypeWithContactCounts)
        {
            var res = contactTypeWithContactCounts == null ? null : new BLL.App.DTO.ContactTypeWithContactCounts()
            {
                Id = contactTypeWithContactCounts.Id,
                ContactTypeValue = contactTypeWithContactCounts.ContactTypeValue,
                ContactCount = contactTypeWithContactCounts.ContactCount
            };
            return res;
        }

    }
}