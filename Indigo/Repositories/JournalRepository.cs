using Indigo.Data;
using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class JournalRepository : IRepository<Journal>
    {
        private readonly ApplicationDbContext _context;
        public JournalRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Journal journal)
        {
            await _context.Journals.AddAsync(journal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Journal = await _context.Journals.FindAsync(id);

            if (Journal == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Journals.Remove(Journal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Journal>> GetAllAsync()
        {
            return await _context.Journals.ToListAsync();
        }

        public Task<IEnumerable<Journal>> GetAllByParentIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Journal> GetByIdAsync(int id)
        {
            var Journal = await _context.Journals.FindAsync(id);

            if (Journal == null)
            {
                throw new KeyNotFoundException();
            }

            return Journal;
        }

        public Task<IEnumerable<Journal>> GetByUserIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Journal journal)
        {
            _context.Journals.Update(journal);
            await _context.SaveChangesAsync();
        }
    }
}
