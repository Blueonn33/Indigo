using Indigo.Models;

namespace Indigo.Repositories.Interfaces
{
    public interface ITomeRepository : IRepository<Tome>
    {
        Task AddTomeAsync(Tome tome);
        Task DeleteTomeAsync(int id);
        Task<Tome> GetTomeByIdAsync(int id);
        Task<IEnumerable<Tome>> GetAllTomesByJournalIdAsync(int journalId);
    }
}
