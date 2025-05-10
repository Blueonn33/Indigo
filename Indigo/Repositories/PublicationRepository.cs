using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class PublicationRepository : Repository<Publication>, IPublicationRepository
    {
        private readonly ApplicationDbContext _context;
        public PublicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddPublicationAsync(Publication publication)
        {
            await _context.Publications.AddAsync(publication);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePublicationAsync(int id)
        {
            var Publication = await _context.Publications.FindAsync(id);

            if (Publication == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Publications.Remove(Publication);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publication>> GetAllPublicationsAsync()
        {
            return await _context.Publications
                .Include(p => p.KeyWords)
                .ToListAsync();
        }

        public async Task<Publication> GetPublicationByIdAsync(int id)
        {
            var Publication = await _context.Publications.FindAsync(id);

            if (Publication == null)
            {
                throw new KeyNotFoundException();
            }

            return Publication;
        }

        public async Task<IEnumerable<Publication>> GetAllPublicationsByPartIdAsync(int partId)
        {
            return await _context.Publications.AsNoTracking().Include(p => p.Part)
                .Where(p => p.PartId == partId)
                .OrderByDescending(p => p.IsApproved).ToListAsync();
        }

        public async Task UpdatePublicationAsync(Publication publication)
        {
            _context.Publications.Update(publication);
            await _context.SaveChangesAsync();
        }
    }
}
