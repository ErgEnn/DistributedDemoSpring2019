using System;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ContactTypeMapper  : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.ContactType))
            {
                return MapFromDAL((DAL.App.DTO.ContactType) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.ContactType))
            {
                return MapFromBLL((BLL.App.DTO.ContactType) inObject) as TOutObject;
            }

            throw new InvalidCastException("No conversion");
        }


        public static BLL.App.DTO.ContactType MapFromDAL(DAL.App.DTO.ContactType contactType)
        {
            var res = new BLL.App.DTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        
        public static DAL.App.DTO.ContactType MapFromBLL(BLL.App.DTO.ContactType contactType)
        {
            var res = new DAL.App.DTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        
        
        public static BLL.App.DTO.ContactTypeWithContactCounts MapFromDAL(DAL.App.DTO.ContactTypeWithContactCounts contactTypeWithContactCounts)
        {
            var res = new BLL.App.DTO.ContactTypeWithContactCounts()
            {
                Id = contactTypeWithContactCounts.Id,
                ContactTypeValue = contactTypeWithContactCounts.ContactTypeValue,
                ContactCount = contactTypeWithContactCounts.ContactCount
            };
            return res;
        }

    }
}