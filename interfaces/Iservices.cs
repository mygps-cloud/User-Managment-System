namespace Ipstatuschecker.interfaces
{
    public interface Iservices<T>where T:class
    {
            Task<bool> AddNewUser(T entety);
          Task<T>UpdateNewUser(T entety);
          Task<bool> DelteUserById(T entety);
           Task<T> GetByUserIdAsync(int id);
          Task<T> GetByUserNameAsync(string name); 
          Task<List<T>> GetAllUsers(); 

    }
}