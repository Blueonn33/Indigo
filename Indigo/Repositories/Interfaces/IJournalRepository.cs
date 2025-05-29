using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories.Interfaces
{
    public interface IJournalRepository : IRepository<Journal>
    {
        Task AddJournalAsync(Journal journal);
        Task DeleteJournalAsync(int id);
        Task<IEnumerable<Journal>> GetAllJournalsAsync();
        Task<Journal> GetJournalByIdAsync(int id);
        Task UpdateJournalAsync(Journal journal);
        Task<IEnumerable<Journal>> GetAllJournalsByTitle(string title);
    }
}
