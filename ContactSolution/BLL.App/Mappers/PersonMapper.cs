using System;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PersonMapper  : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.Person))
            {
                return MapFromDAL((DAL.App.DTO.Person) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.Person))
            {
                return MapFromBLL((BLL.App.DTO.Person) inObject) as TOutObject;
            }

            throw new InvalidCastException("No conversion");
        }


        public static BLL.App.DTO.Person MapFromDAL(DAL.App.DTO.Person contactType)
        {
            throw new NotImplementedException();
            return null;
        }
        
        public static DAL.App.DTO.Person MapFromBLL(BLL.App.DTO.Person contactType)
        {
            throw new NotImplementedException();
            return null;
        }

    }
}