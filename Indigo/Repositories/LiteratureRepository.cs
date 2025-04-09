using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class LiteratureRepository : Repository<Literature>, ILiteratureRepository
    {
        private readonly ApplicationDbContext _context;
        public LiteratureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddLiteratureAsync(Literature Literature)
        {
            await _context.Literatures.AddAsync(Literature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLiteratureAsync(int id)
        {
            var Literature = await _context.Literatures.FindAsync(id);

            if (Literature == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Literatures.Remove(Literature);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Literature>> GetAllLiteraturesByPublicationIdAsync(int publicationId)
        {
            return await _context.Literatures.AsNoTracking().Include(p => p.Publication)
                .Where(p => p.PublicationId == publicationId)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Literature> GetLiteratureByIdAsync(int id)
        {
            var Literature = await _context.Literatures.FindAsync(id);

            if (Literature == null)
            {
                throw new KeyNotFoundException();
            }

            return Literature;
        }

        public async Task UpdateLiteratureAsync(Literature entity)
        {
            _context.Literatures.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
