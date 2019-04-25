using System;
using Contracts.DAL.Base.Mappers;
using Domain;

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

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static DAL.App.DTO.Person MapFromDomain(Domain.Person person)
        {
            var res = person == null ? null : new DAL.App.DTO.Person()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                AppUserId = person.AppUserId,
            };

            return res;
        }
        
        public static Domain.Person MapFromDAL(DAL.App.DTO.Person person)
        {
            var res = person == null ? null : new Domain.Person()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                AppUserId = person.AppUserId,
            };

            return res;
        }

    }
}