namespace Contracts.BLL.Base.Helpers
{
    public interface IBaseBLLMapper
    {
        TOutObject Map<TOutObject>(object inObject); 
    }
}