using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Indigo.Controllers
{
    public class PartsController : Controller
    {
        private readonly IPartRepository _partRepository;
        public PartsController(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int tomeId)
        {
            if(tomeId == 0)
            {
                return NotFound();
            }

            var parts = await _partRepository.GetAllPartsByTomeIdAsync(tomeId);
            ViewBag.TomeId = tomeId;
            return View(parts);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Publisher")]
        public async Task<IActionResult> Create(PartViewModel partVm, int tomeId)
        {
            if(ModelState.IsValid)
            {
                var part = new Part
                {
                    Title = partVm.Title,
                    TomeId = tomeId
                };
                await _partRepository.AddPartAsync(part);
                return RedirectToAction(nameof(Index), new { tomeId });
            }
            return View(partVm);
        }
    }
}
