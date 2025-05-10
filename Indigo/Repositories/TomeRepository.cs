using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class TomeRepository : Repository<Tome>, ITomeRepository
    {
        private readonly ApplicationDbContext _context;
        public TomeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddTomeAsync(Tome tome)
        {
            await _context.Tomes.AddAsync(tome);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTomeAsync(int id)
        {
            var Tome = await _context.Tomes.FindAsync(id);

            if (Tome == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Tomes.Remove(Tome);
            await _context.SaveChangesAsync();
        }
        public async Task<Tome> GetTomeByIdAsync(int id)
        {
            var Tome = await _context.Tomes.FindAsync(id);

            if (Tome == null)
            {
                throw new KeyNotFoundException();
            }

            return Tome;
        }
        public async Task<IEnumerable<Tome>> GetAllTomesByJournalIdAsync(int journalId)
        {
            return await _context.Tomes.AsNoTracking().Include(p => p.Journal)
                .Where(t => t.JournalId == journalId)
                .OrderBy(t => t.Title).ToListAsync();
        }
    }
}
