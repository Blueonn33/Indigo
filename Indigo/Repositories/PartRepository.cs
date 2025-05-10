using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class PartRepository : Repository<Part>, IPartRepository
    {
        private readonly ApplicationDbContext _context;
        public PartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddPartAsync(Part part)
        {
            await _context.Parts.AddAsync(part);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePartAsync(int id)
        {
            var Part = await _context.Parts.FindAsync(id);

            if (Part == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Parts.Remove(Part);
            await _context.SaveChangesAsync();
        }
        public async Task<Part> GetPartByIdAsync(int id)
        {
            var Part = await _context.Parts.FindAsync(id);

            if (Part == null)
            {
                throw new KeyNotFoundException();
            }

            return Part;
        }
        public async Task<IEnumerable<Part>> GetAllPartsByTomeIdAsync(int tomeId)
        {
            return await _context.Parts.AsNoTracking().Include(p => p.Tome)
                .Where(t => t.TomeId == tomeId)
                .OrderBy(t => t.Title).ToListAsync();
        }
    }
}
