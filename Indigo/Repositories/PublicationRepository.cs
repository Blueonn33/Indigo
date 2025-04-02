using Indigo.Data;
using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class PublicationRepository : IRepository<Publication>
    {
        private readonly ApplicationDbContext _context;
        public PublicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Publication publication)
        {
            await _context.Publications.AddAsync(publication);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Publication = await _context.Publications.FindAsync(id);

            if (Publication == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Publications.Remove(Publication);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publication>> GetAllAsync()
        {
            return await _context.Publications.ToListAsync();
        }

        public async Task<Publication> GetByIdAsync(int id)
        {
            var Publication = await _context.Publications.FindAsync(id);

            if (Publication == null)
            {
                throw new KeyNotFoundException();
            }

            return Publication;
        }

        public async Task<IEnumerable<Publication>> GetAllByParentIdAsync(int journalId)
        {
            return await _context.Publications.AsNoTracking().Include(p => p.Journal)
                .Where(p => p.JournalId == journalId)
                .OrderBy(p => p.Title).ToListAsync();
        }

        public async Task UpdateAsync(Publication publication)
        {
            _context.Publications.Update(publication);
            await _context.SaveChangesAsync();
        }
    }
}
