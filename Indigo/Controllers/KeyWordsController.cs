using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Indigo.Controllers
{
    public class KeyWordsController : Controller
    {
        private readonly IKeyWordRepository _repository;

        public KeyWordsController(IKeyWordRepository repository)
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

            var keyWords = await _repository.GetAllKeyWordsByPublicationIdAsync(publicationId);
            ViewBag.PublicationId = publicationId; // Предаваме ID-то към изгледа
            return View(keyWords);
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
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Author")]
        public async Task<IActionResult> Create(KeyWordViewModel keyWordVm, int publicationId)
        {
            if (publicationId == 0)
            {
                return BadRequest("Не е предоставено валидно ID на публикация.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.PublicationId = publicationId;
                return View(keyWordVm);
            }

            var keyWord = new KeyWord
            {
                Value = keyWordVm.Value,
                PublicationId = publicationId
            };

            await _repository.AddKeyWordAsync(keyWord);
            return RedirectToAction(nameof(Index), new { publicationId });
        }
    }
}
