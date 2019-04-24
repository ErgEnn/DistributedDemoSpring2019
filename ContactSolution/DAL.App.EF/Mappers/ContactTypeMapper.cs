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

            throw new InvalidCastException("No conversion");
        }


        public static DAL.App.DTO.ContactType MapFromDomain(Domain.ContactType contactType)
        {
            throw new InvalidCastException("No conversion");
            return null;
        }
        
        public static Domain.ContactType MapFromDAL(DAL.App.DTO.ContactType contactType)
        {
            throw new InvalidCastException("No conversion");
            return null;
        }
        


    }
}