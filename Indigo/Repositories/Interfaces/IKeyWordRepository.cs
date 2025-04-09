using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories.Interfaces
{
    public interface IKeyWordRepository : IRepository<KeyWord>
    {
        Task AddKeyWordAsync(KeyWord keyWord);
        Task<IEnumerable<KeyWord>> GetAllKeyWordsByPublicationIdAsync(int publicationId);
    }
}
