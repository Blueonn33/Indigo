using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace Indigo.Controllers
{
    public class TomesController : Controller
    {
        private readonly ITomeRepository _tomeRepository;
        public TomesController(ITomeRepository tomeRepository)
        {
            _tomeRepository = tomeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int journalId)
        {
            if(journalId == 0)
            {
                return NotFound();
            }

            var tomes = await _tomeRepository.GetAllTomesByJournalIdAsync(journalId);
            return View(tomes);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> Create([FromForm] TomeViewModel tomeVm, int journalId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Невалидни данни");

            var tome = new Tome
            {
                Title = tomeVm.Title,
                Description = tomeVm.Description,
                JournalId = journalId
            };

            await _tomeRepository.AddTomeAsync(tome);

            var tomes = await _tomeRepository.GetAllTomesByJournalIdAsync(journalId);
            
            return PartialView("_TomeListPartial", tomes);
        }

    }
}
