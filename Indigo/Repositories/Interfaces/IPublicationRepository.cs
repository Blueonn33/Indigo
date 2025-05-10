using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories.Interfaces
{
    public interface IPublicationRepository : IRepository<Publication>
    {
        Task AddPublicationAsync(Publication publication);
        Task DeletePublicationAsync(int id);
        Task<IEnumerable<Publication>> GetAllPublicationsAsync();
        Task<Publication> GetPublicationByIdAsync(int id);
        Task<IEnumerable<Publication>> GetAllPublicationsByPartIdAsync(int partId);
        Task UpdatePublicationAsync(Publication publication);
    }
}
