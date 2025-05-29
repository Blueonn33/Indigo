using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class KeyWordRepository : Repository<KeyWord>, IKeyWordRepository
    {
        private readonly ApplicationDbContext _context;
        public KeyWordRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddKeyWordAsync(KeyWord keyWord)
        {
            await _context.KeyWords.AddAsync(keyWord);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<KeyWord>> GetAllKeyWordsByPublicationIdAsync(int publicationId)
        {
            return await _context.KeyWords
                .AsNoTracking()     // данните са само за четене
                .Include(p => p.Publication)
                .Where(p => p.PublicationId == publicationId)
                .OrderBy(p => p.Value)
                .ToListAsync();
        }
    }
}
