using Indigo.Models;
using Indigo.ViewModels;

namespace Indigo.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> FindAsync(params object[] keyValues);
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(params object[] keyValues);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
