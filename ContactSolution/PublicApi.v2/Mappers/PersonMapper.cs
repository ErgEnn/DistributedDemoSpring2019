using System;
using externalDTO = PublicApi.v2.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v2.Mappers
{
    public class PersonMapper  
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Person))
            {
                // map internalDTO to externalDTO
                return MapFromBLL((internalDTO.Person) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Person))
            {
                // map externalDTO to internalDTO
                return MapFromExternal((externalDTO.Person) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.Person MapFromBLL(internalDTO.Person person)
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
        
        public static internalDTO.Person MapFromExternal(externalDTO.Person person)
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