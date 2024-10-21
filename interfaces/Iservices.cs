namespace Ipstatuschecker.interfaces
{
    public interface Iservices<T>where T:class
    {
            Task<T> AddNewUser(T entety);
          Task<T>UpdateNewUser(T entety);
          Task<T> DelteUserById(T entety);
           Task<T> GetByUserIdAsync(int id);
          Task<T> GetByUserNameAsync(string name); 
          Task<List<T>> GetAllUsers(); 

    }
}