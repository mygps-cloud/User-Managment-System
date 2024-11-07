namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface ITimeControl<TRequest, TResult> where TRequest : class
    {
        Task<TResult> TimeControlResult(TRequest entity,bool Status);
    }
}
