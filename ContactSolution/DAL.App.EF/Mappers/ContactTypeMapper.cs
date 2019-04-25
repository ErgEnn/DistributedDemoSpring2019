using System;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ContactTypeMapper  : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.ContactType))
            {
                return MapFromDomain((Domain.ContactType) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.ContactType))
            {
                return MapFromDAL((DAL.App.DTO.ContactType) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static DAL.App.DTO.ContactType MapFromDomain(Domain.ContactType contactType)
        {
            var res = contactType == null ? null : new DAL.App.DTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        
        public static Domain.ContactType MapFromDAL(DAL.App.DTO.ContactType contactType)
        {
            var res = contactType == null ? null : new Domain.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue
            };
            return res;
        }
        


    }
}