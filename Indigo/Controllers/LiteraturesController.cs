using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Indigo.Controllers
{
    public class LiteraturesController : Controller
    {
        private readonly ILiteratureRepository _repository;

        public LiteraturesController(ILiteratureRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int publicationId)
        {
            if (publicationId == 0)
            {
                return NotFound();
            }

            var literatures = await _repository.GetAllLiteraturesByPublicationIdAsync(publicationId);
            ViewBag.PublicationId = publicationId; // Предаваме ID-то към изгледа
            return View(literatures);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult Create(int publicationId)
        {
            if (publicationId == 0)
            {
                return NotFound();
            }

            ViewBag.PublicationId = publicationId; // Предаваме ID-то към изгледа
            return View(new LiteratureViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Author")]
        public async Task<IActionResult> Create(LiteratureViewModel literatureVm, int publicationId)
        {
            if (publicationId == 0)
            {
                return BadRequest("Не е предоставено валидно ID на публикация.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.PublicationId = publicationId;
                return View(literatureVm);
            }

            var literature = new Literature
            {
                Name = literatureVm.Name,
                Content = literatureVm.Content,
                PublicationId = publicationId
            };

            await _repository.AddLiteratureAsync(literature);
            return RedirectToAction(nameof(Index), new { publicationId });
        }
    }
}
