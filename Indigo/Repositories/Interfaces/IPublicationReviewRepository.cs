using Indigo.Models;

namespace Indigo.Repositories.Interfaces
{
    public interface IPublicationReviewRepository : IRepository<PublicationReview>
    {
        Task AddPublicationReviewAsync(PublicationReview review);
        Task<IEnumerable<PublicationReview>> GetAllReviewsByPublicationIdAsync(int publicationId);
    }
}
