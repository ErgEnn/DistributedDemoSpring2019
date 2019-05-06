using System;
using ee.itcollege.akaver.Contracts.DAL.Base.Mappers;
using internalDTO = Domain;
using externalDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ContactTypeMapper  : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ContactType))
            {
                return MapFromDomain((internalDTO.ContactType) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ContactType))
            {
                return MapFromDAL((externalDTO.ContactType) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ContactType MapFromDomain(internalDTO.ContactType contactType)
        {
            var res = contactType == null ? null : new externalDTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = contactType.ContactTypeValue.Translate()
            };
            return res;
        }
        
        public static internalDTO.ContactType MapFromDAL(externalDTO.ContactType contactType)
        {
            var res = contactType == null ? null : new internalDTO.ContactType()
            {
                Id = contactType.Id,
                ContactTypeValue = new internalDTO.MultiLangString(contactType.ContactTypeValue) 
            };
            return res;
        }
        

        

    }
}