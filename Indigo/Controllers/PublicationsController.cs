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
        private readonly IKeyWordRepository _keyWordsRepository;

        public PublicationsController(IPublicationRepository repository, IKeyWordRepository keyWordRepository)
        {
            _repository = repository;
            _keyWordsRepository = keyWordRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int partId)
        {
            if (partId == 0)
            {
                return NotFound();
            }

            var publications = await _repository.GetAllPublicationsByPartIdAsync(partId);
            ViewBag.PartId = partId;
            return View(publications);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult Create(int partId)
        {
            if (partId == 0)
            {
                return NotFound();
            }

            ViewBag.PartId = partId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Author")]
        public async Task<IActionResult> Create(PublicationViewModel publicationVm, int partId)
        {
            if (partId == 0)
            {
                return BadRequest("Не е предоставено валидно ID на списание.");
            }

            if (ModelState.IsValid)
            {
                byte[]? pdfData = null;
                string? mimeType = null;
                string? fileName = null;

                if (publicationVm.PdfFile != null && publicationVm.PdfFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await publicationVm.PdfFile.CopyToAsync(ms);
                        pdfData = ms.ToArray();
                        mimeType = publicationVm.PdfFile.ContentType;
                        fileName = publicationVm.PdfFile.FileName;
                    }
                }

                var publication = new Publication
                {
                    Title = publicationVm.Title,
                    Topic = publicationVm.Topic,
                    Description = publicationVm.Description,
                    AuthorName = publicationVm.AuthorName,
                    IsApproved = publicationVm.IsApproved,
                    PartId = partId,
                    PdfFileData = pdfData,
                    PdfMimeType = mimeType,
                    PdfFileName = fileName
                };

                await _repository.AddPublicationAsync(publication);
                return RedirectToAction(nameof(Index), new { partId });
            }

            ViewBag.PartId = partId;
            return View(publicationVm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPdf(int publicationId)
        {
            var publication = await _repository.GetPublicationByIdAsync(publicationId);
            if (publication == null || publication.PdfFileData == null || publication.PdfMimeType == null)
            {
                return NotFound();
            }

            return File(publication.PdfFileData, publication.PdfMimeType);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PdfViewer(int publicationId)
        {
            return View(publicationId);
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

            ViewBag.PartId = publication.PartId;

            return View(publication);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Reviewer")]
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string title)
        {
            var allPublications = await _repository.GetAllPublicationsAsync();

            if (string.IsNullOrWhiteSpace(title))
            {
                return View("Index", allPublications); // ако няма търсене, върни всички
            }

            var filteredPublications = allPublications
                .Where(j => j.AuthorName.Contains(title, StringComparison.OrdinalIgnoreCase) ||
                            j.Title.Contains(title, StringComparison.OrdinalIgnoreCase) ||
                            j.Topic.Contains(title, StringComparison.OrdinalIgnoreCase) ||
                            j.KeyWords != null && j.KeyWords.Any(k => k.Value.Contains(title, 
                            StringComparison.OrdinalIgnoreCase)));


            return View("Index", filteredPublications); // използваме същата View като Index
        }
    }
}
