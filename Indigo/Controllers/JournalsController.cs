using Indigo.Constants;
using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories;
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
        private readonly IRepository<Journal> _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public JournalsController(IRepository<Journal> repository, 
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // в момента извлича всички записи, което е неефктивно;
            // трябва да се използва метод GetAllByUserIdAsync()s

            if (User.IsInRole(Roles.Publisher))
            {
                var allJournals = await _repository.GetAllAsync();
                var userId = _userManager.GetUserId(User);
                var filteredJournals = allJournals.Where(j => j.UserId == userId);

                return View(filteredJournals);
            }

            var journals = await _repository.GetAllAsync();
            return View(journals);
        }

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
                var journal = new Journal
                {
                    Title = journalVm.Title,
                    Description = journalVm.Description,
                    ImageUrl = journalVm.ImageUrl,
                    UserId = _userManager.GetUserId(User)
                };

                await _repository.AddAsync(journal);
                return RedirectToAction(nameof(Index));
            }

            return View(journalVm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Journal journal = await _repository.GetByIdAsync(id);

            if (journal == null)
            {
                return NotFound();
            }

            var journalVm = new JournalViewModel
            {
                Title = journal.Title,
                Description = journal.Description,
                ImageUrl = journal.ImageUrl
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
                var journal = await _repository.GetByIdAsync(id);

                if (journal == null)
                {
                    return NotFound();
                }

                journal.Title = journalVm.Title;
                journal.Description = journalVm.Description;
                journal.ImageUrl = journalVm.ImageUrl;

                await _repository.UpdateAsync(journal);

                return RedirectToAction(nameof(Index));
            }

            return View(journalVm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Journal(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Journal journal = await _repository.GetByIdAsync(id);

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
            var journal = await _repository.GetByIdAsync(id);

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
    }
}
