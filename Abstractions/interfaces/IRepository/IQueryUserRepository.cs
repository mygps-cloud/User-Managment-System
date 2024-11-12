using System.Collections.Generic;
namespace Abstractions.interfaces.IRepository
{
    public interface IQueryUserRepository<T> where T : class
    {
        Task<List<T>> GetByIdTolist(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task<List<T>> GetAll();
    }
}
