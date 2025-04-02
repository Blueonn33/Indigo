using Indigo.Models;

namespace Indigo.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        //Task<IEnumerable<T>> GetByUserIdAsync(string id);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllByParentIdAsync(int id);

    }
}
