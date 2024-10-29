namespace Abstractions.interfaces
{
    public interface IQueryIpStatusRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name); 
        Task<List<T>> GetAll(); 
    }
}
