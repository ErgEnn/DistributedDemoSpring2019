using AutoMapper;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;

namespace DAL.Base.EF.Helpers
{
    public class BaseDALMapper<TDALEntity, TDomainEntity> : IBaseDALMapper
        where TDALEntity : class, new()
        where TDomainEntity : class, IBaseEntity, new()
    {
        private readonly IMapper _mapper;

        public BaseDALMapper()
        {
            _mapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TDALEntity, TDomainEntity>();
                    cfg.CreateMap<TDomainEntity, TDALEntity>();
                })
                .CreateMapper();
        }

        public TOutObject Map<TOutObject>(object inObject)
        {
            return _mapper.Map<TOutObject>(inObject);
        }
        
    }
}