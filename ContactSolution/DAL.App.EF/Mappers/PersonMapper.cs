using System;
using ee.itcollege.akaver.Contracts.DAL.Base.Mappers;
using internalDTO = Domain;
using externalDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class PersonMapper  : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Person))
            {
                return MapFromDomain((internalDTO.Person) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Person))
            {
                return MapFromDAL((externalDTO.Person) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.Person MapFromDomain(internalDTO.Person person)
        {
            var res = person == null ? null : new externalDTO.Person()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                AppUserId = person.AppUserId,
            };

            return res;
        }
        
        public static internalDTO.Person MapFromDAL(externalDTO.Person person)
        {
            var res = person == null ? null : new internalDTO.Person()
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