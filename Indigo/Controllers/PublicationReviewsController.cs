using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Indigo.Controllers
{
    public class PublicationReviewsController : Controller
    {
        private readonly IPublicationReviewRepository _repository;
        private readonly IPublicationRepository _publicationRepository;

        public PublicationReviewsController(IPublicationReviewRepository repository,
            IPublicationRepository publicationRepository)
        {
            _repository = repository;
            _publicationRepository = publicationRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int publicationId)
        {
            if (publicationId == 0)
            {
                return NotFound();
            }

            var publicationReviews = await _repository.GetAllReviewsByPublicationIdAsync(publicationId);
            var publication = await _publicationRepository.GetPublicationByIdAsync(publicationId);

            ViewBag.PublicationId = publicationId;
            ViewBag.PartId = publication.PartId;
            return View(publicationReviews);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Reviewer")]
        public IActionResult Create(int publicationId)
        {
            if (publicationId == 0)
            {
                return NotFound();
            }

            ViewBag.PublicationId = publicationId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Reviewer")]
        public async Task<IActionResult> Create(PublicationReviewViewModel reviewVm, int publicationId)
        {
            if (publicationId == 0)
            {
                return BadRequest("Не е предоставено валидно ID на публикация.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.PublicationId = publicationId;
                return View(reviewVm);
            }

            var publicationReview = new PublicationReview
            {
                Title = reviewVm.Title,
                Comment = reviewVm.Comment,
                ReviewerName = reviewVm.ReviewerName,
                ReviewerEmail = reviewVm.ReviewerEmail,
                ReviewDate = DateTime.Now,
                PublicationId = publicationId
            };

            await _repository.AddPublicationReviewAsync(publicationReview);
            return RedirectToAction(nameof(Index), new { publicationId });
        }
    }
}
