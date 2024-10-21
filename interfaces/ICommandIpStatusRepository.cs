namespace Ipstatuschecker.interfaces
{
    public interface ICommandIpStatusRepository<T>where T:class
    {
          Task<bool> CreateUser(T entety);
          Task<T>UpdateUser(T entety);
          Task<bool> DelteUser(int entetyId);
         
    }
}