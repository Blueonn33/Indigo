using Indigo.Data;
using Indigo.Models;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class LiteratureRepository : IRepository<Literature>
    {
        private readonly ApplicationDbContext _context;
        public LiteratureRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Literature Literature)
        {
            await _context.Literatures.AddAsync(Literature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Literature = await _context.Literatures.FindAsync(id);

            if (Literature == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Literatures.Remove(Literature);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Literature>> GetAllAsync()
        {
            return await _context.Literatures.ToListAsync();
        }

        public async Task<IEnumerable<Literature>> GetAllByParentIdAsync(int publicationId)
        {
            return await _context.Literatures.AsNoTracking().Include(p => p.Publication)
                .Where(p => p.PublicationId == publicationId)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Literature> GetByIdAsync(int id)
        {
            var Literature = await _context.Literatures.FindAsync(id);

            if (Literature == null)
            {
                throw new KeyNotFoundException();
            }

            return Literature;
        }

        public async Task UpdateAsync(Literature entity)
        {
            _context.Literatures.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
