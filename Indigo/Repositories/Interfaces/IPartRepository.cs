using Indigo.Models;

namespace Indigo.Repositories.Interfaces
{
    public interface IPartRepository : IRepository<Part>
    {
        Task AddPartAsync(Part part);
        Task DeletePartAsync(int id);
        Task<Part> GetPartByIdAsync(int id);
        Task<IEnumerable<Part>> GetAllPartsByTomeIdAsync(int tomeId);
    }
}
