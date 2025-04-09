using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories.Interfaces
{
    public interface ILiteratureRepository : IRepository<Literature>
    {
        Task AddLiteratureAsync(Literature Literature);
        Task DeleteLiteratureAsync(int id);
        Task<IEnumerable<Literature>> GetAllLiteraturesByPublicationIdAsync(int publicationId);
        Task<Literature> GetLiteratureByIdAsync(int id);  
        Task UpdateLiteratureAsync(Literature entity);
    }
}
