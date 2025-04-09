using Indigo.Constants;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Controllers
{
    public class PublicationsController : Controller
    {
        private readonly IPublicationRepository _repository;

        public PublicationsController(IPublicationRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("{journalId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int journalId)
        {
            if (journalId == 0)
            {
                return NotFound();
            }

            var publications = await _repository.GetAllPublicationsByJournalIdAsync(journalId);
            return View(publications);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult Create(int journalId)
        {
            if (journalId == 0)
            {
                return NotFound();
            }

            ViewBag.JournalId = journalId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Author")]
        public async Task<IActionResult> Create(PublicationViewModel publicationVm, int journalId)
        {
            if (journalId == 0)
            {
                return BadRequest("Не е предоставено валидно ID на списание.");
            }

            if (ModelState.IsValid)
            {
                var publication = new Publication
                {
                    Title = publicationVm.Title,
                    Topic = publicationVm.Topic,
                    Description = publicationVm.Description,
                    Content = publicationVm.Content,
                    AuthorName = publicationVm.AuthorName,
                    IsApproved = publicationVm.IsApproved,
                    JournalId = journalId
                };

                await _repository.AddPublicationAsync(publication);
                return RedirectToAction(nameof(Index), new { journalId });
            }

            ViewBag.JournalId = journalId;
            return View(publicationVm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Publication(int publicationId)
        {
            if (publicationId == 0)
            {
                return NotFound();
            }

            var publication = await _repository.GetPublicationByIdAsync(publicationId);

            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> ApprovePublication(int publicationId)
        {
            if(publicationId == 0)
            {
                return NotFound();
            }

            var publication = await _repository.GetPublicationByIdAsync(publicationId);

            if(publication == null)
            {
                return NotFound();
            }

            publication.IsApproved = true;
            await _repository.UpdatePublicationAsync(publication);

            return RedirectToAction(nameof(Index));
        }
    }
}
