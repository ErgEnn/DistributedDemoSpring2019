using System;
using Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO;
using externalDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PersonMapper  : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Person))
            {
                return MapFromDAL((internalDTO.Person) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Person))
            {
                return MapFromBLL((externalDTO.Person) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.Person MapFromDAL(internalDTO.Person person)
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
        
        public static internalDTO.Person MapFromBLL(externalDTO.Person person)
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