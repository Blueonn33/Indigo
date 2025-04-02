using Indigo.Constants;
using Indigo.Models;
using Indigo.Repositories;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Controllers
{
    public class PublicationsController : Controller
    {
        private readonly IRepository<Publication> _repository;

        public PublicationsController(IRepository<Publication> repository)
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

            var publications = await _repository.GetAllByParentIdAsync(journalId);
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
                    JournalId = journalId
                };

                await _repository.AddAsync(publication);
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

            var publication = await _repository.GetByIdAsync(publicationId);

            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }
    }
}
