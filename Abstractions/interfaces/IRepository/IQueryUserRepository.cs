namespace Abstractions.interfaces.IRepository
{
    public interface IQueryUserRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name); 
        Task<List<T>> GetAll(); 
    }
}
