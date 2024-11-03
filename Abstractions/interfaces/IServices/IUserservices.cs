namespace Abstractions.interfaces.Iservices
{
    public interface IUserservices<T>where T:class
    {
           Task<bool> AddNewUser(T entety);
          Task<T>UpdateNewUser(T entety);
          Task<bool> DelteUserById(int entetyId);
           Task<T> GetByUserIdAsync(int id);
          Task<T> GetByUserNameAsync(string name); 
          Task<List<T>> GetAllUsers(); 

    }
}