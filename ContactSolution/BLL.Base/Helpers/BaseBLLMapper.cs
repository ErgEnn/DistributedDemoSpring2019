using AutoMapper;
using Contracts.BLL.Base.Helpers;
using Contracts.DAL.Base.Helpers;

namespace BLL.Base.Helpers
{
    public class BaseBLLMapper<TBLLEntity, TDALEntity> : IBaseBLLMapper
        where TBLLEntity : class, new()
        where TDALEntity : class, new()
    {
        private readonly IMapper _mapper;

        public BaseBLLMapper()
        {
            _mapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TBLLEntity, TDALEntity>();
                    cfg.CreateMap<TDALEntity, TBLLEntity>();
                })
                .CreateMapper();
        }

        public TOutObject Map<TOutObject>(object inObject)
        {
            return _mapper.Map<TOutObject>(inObject);
        }
    }
}