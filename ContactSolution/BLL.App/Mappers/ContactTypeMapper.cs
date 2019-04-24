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
            throw new NotImplementedException();
            return null;
        }
        
        public static DAL.App.DTO.ContactType MapFromBLL(BLL.App.DTO.ContactType contactType)
        {
            throw new NotImplementedException();
            return null;
        }
        
        
        public static BLL.App.DTO.ContactTypeWithContactCounts MapFromDAL(DAL.App.DTO.ContactTypeWithContactCounts contactTypeWithContactCounts)
        {
            throw new NotImplementedException();
            return null;
        }

    }
}