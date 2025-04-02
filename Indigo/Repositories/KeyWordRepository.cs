using Indigo.Data;
using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class KeyWordRepository : IRepository<KeyWord>
    {
        private readonly ApplicationDbContext _context;
        public KeyWordRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(KeyWord keyWord)
        {
            await _context.KeyWords.AddAsync(keyWord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var keyWord = await _context.KeyWords.FindAsync(id);
                
            if (keyWord == null)
            {
                throw new KeyNotFoundException();
            }

            _context.KeyWords.Remove(keyWord);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<KeyWord>> GetAllAsync()
        {
            return await _context.KeyWords.ToListAsync();
        }

        public async Task<IEnumerable<KeyWord>> GetAllByParentIdAsync(int publicationId)
        {
            return await _context.KeyWords.AsNoTracking().Include(p => p.Publication)
                .Where(p => p.PublicationId == publicationId)
                .OrderBy(p => p.Value).ToListAsync();
        }

        public async Task<KeyWord> GetByIdAsync(int id)
        {
            var keyWord = await _context.KeyWords.FindAsync(id);

            if (keyWord == null)
            {
                throw new KeyNotFoundException();
            }

            return keyWord;
        }

        public async Task UpdateAsync(KeyWord entity)
        {
            _context.KeyWords.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
