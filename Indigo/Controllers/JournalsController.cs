using Indigo.Constants;
using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Controllers
{
    [Authorize]
    public class JournalsController : Controller
    {
        private readonly IJournalRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public JournalsController(IJournalRepository repository, 
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Publisher))
            {
                var allJournals = await _repository.GetAllJournalsAsync();
                var userId = _userManager.GetUserId(User);
                var filteredJournals = allJournals.Where(j => j.UserId == userId);

                return View(filteredJournals);
            }

            var journals = await _repository.GetAllJournalsAsync();
            return View(journals);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Publisher")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> Create(JournalViewModel journalVm)
        {
            if (ModelState.IsValid)
            {
                byte[]? imageData = null;
                string? mimeType = null;

                if (journalVm.ImageFile != null && journalVm.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await journalVm.ImageFile.CopyToAsync(ms);
                        imageData = ms.ToArray();
                        mimeType = journalVm.ImageFile.ContentType;
                    }
                }

                var journal = new Journal
                {
                    Title = journalVm.Title,
                    Description = journalVm.Description,
                    ISSN_Online = journalVm.ISSN_Online,
                    ISSN_Print = journalVm.ISSN_Print,
                    License = journalVm.License,
                    ImageData = imageData,
                    ImageMimeType = mimeType,
                    UserId = _userManager.GetUserId(User),
                };

                await _repository.AddAsync(journal);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Journal journal = await _repository.GetJournalByIdAsync(id);

            if (journal == null)
            {
                return NotFound();
            }

            var journalVm = new JournalViewModel
            {
                Title = journal.Title,
                Description = journal.Description,
                ISSN_Online = journal.ISSN_Online,
                ISSN_Print = journal.ISSN_Print,
                License = journal.License,
            };

            return View(journalVm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> Edit(JournalViewModel journalVm, int id)
        {
            if (journalVm != null && journalVm.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Заглавието е твърде кратко");
            }

            if (ModelState.IsValid)
            {
                var journal = await _repository.GetJournalByIdAsync(id);

                if (journal == null)
                {
                    return NotFound();
                }

                journal.Title = journalVm.Title;
                journal.Description = journalVm.Description;
                journal.ISSN_Online = journalVm.ISSN_Online;
                journal.ISSN_Print = journalVm.ISSN_Print;
                journal.License = journalVm.License;

                if (journalVm.ImageFile != null && journalVm.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await journalVm.ImageFile.CopyToAsync(ms);
                        journal.ImageData = ms.ToArray();
                        journal.ImageMimeType = journalVm.ImageFile.ContentType;
                    }
                }

                await _repository.UpdateAsync(journal);
                return RedirectToAction(nameof(Index));
            }
            return View(journalVm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Journal(int journalId)
        {
            if (journalId == 0)
            {
                return NotFound();
            }

            Journal journal = await _repository.GetJournalByIdAsync(journalId);

            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> Delete(int id)
        {
            var journal = await _repository.GetJournalByIdAsync(id);

            if (journal == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (User.IsInRole(Roles.Admin) == false && journal.UserId != userId)
            {
                return Forbid();
            }

            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string title)
        {
            var allJournals = await _repository.GetAllJournalsAsync();

            if (string.IsNullOrWhiteSpace(title))
            {
                return View("Index", allJournals);
            }

            var filteredJournals = allJournals
                .Where(j => j.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            return View("Index", filteredJournals);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(int id)
        {
            var journal = await _repository.GetJournalByIdAsync(id);

            if (journal == null || journal.ImageData == null || journal.ImageMimeType == null)
            {
                return NotFound();
            }

            return File(journal.ImageData, journal.ImageMimeType);
        }
    }
}
