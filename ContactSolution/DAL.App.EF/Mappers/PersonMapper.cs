using System;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PersonMapper  : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.Person))
            {
                return MapFromDomain((Domain.Person) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Person))
            {
                return MapFromDAL((DAL.App.DTO.Person) inObject) as TOutObject;
            }

            throw new InvalidCastException("No conversion");
        }


        public static DAL.App.DTO.Person MapFromDomain(Domain.Person contactType)
        {
            throw new InvalidCastException("No conversion");
            return null;
        }
        
        public static Domain.Person MapFromDAL(DAL.App.DTO.Person contactType)
        {
            throw new InvalidCastException("No conversion");
            return null;
        }

    }
}