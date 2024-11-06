namespace Abstractions.interfaces
{
    public interface ICommandUserRepository<T>where T:class
    {
          Task<bool> Create(T entety);
          Task<T>Update(T entety);
          Task<bool> Delete(int entetyId);
         
    }
}