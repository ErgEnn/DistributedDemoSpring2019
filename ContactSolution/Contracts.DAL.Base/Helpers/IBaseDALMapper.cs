namespace Contracts.DAL.Base.Helpers
{
    public interface IBaseDALMapper
    {
        TOutObject Map<TOutObject>(object inObject); 
    }
}