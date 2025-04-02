﻿using Indigo.Models;
using Indigo.Repositories;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Indigo.Controllers
{
    public class KeyWordsController : Controller
    {
        private readonly IRepository<KeyWord> _repository;

        public KeyWordsController(IRepository<KeyWord> repository)
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

            var keyWords = await _repository.GetAllByParentIdAsync(publicationId);
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
            return View(new KeyWordViewModel());
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

            await _repository.AddAsync(keyWord);
            return RedirectToAction(nameof(Index), new { publicationId });
        }
    }
}
