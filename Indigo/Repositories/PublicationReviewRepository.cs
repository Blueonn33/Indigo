using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class PublicationReviewRepository : Repository<PublicationReview>, IPublicationReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public PublicationReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddPublicationReviewAsync(PublicationReview review)
        {
            await _context.PublicationReviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PublicationReview>> GetAllReviewsByPublicationIdAsync(int publicationId)
        {
            return await _context.PublicationReviews.AsNoTracking().Include(p => p.Publication)
               .Where(p => p.PublicationId == publicationId)
               .OrderByDescending(p => p.ReviewDate).ToListAsync();
        }
    }
}
