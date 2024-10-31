namespace Abstractions.interfaces
{
    public interface ICommandIpStatusRepository<T>where T:class
    {
          Task<bool> Create(T entety);
          Task<T>Update(T entety);
          Task<bool> Delete(int entetyId);
         
    }
}