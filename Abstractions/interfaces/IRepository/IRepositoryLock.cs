
namespace Ipstatuschecker.Abstractions.interfaces.IRepository
{

    public interface IRepositoryLock<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> Save();
       
    }
  
}