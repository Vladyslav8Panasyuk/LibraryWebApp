using LibraryWebApp.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryWebApp.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerService;
        private readonly ILibraryService _libraryService;

        public ReaderController(IReaderService readerService, ILibraryService libraryService)
        {
            _readerService = readerService;
            _libraryService = libraryService;
        }

        public async Task<IActionResult> Index(int? libraryId)
        {
            if (!libraryId.HasValue)
            {
                return NotFound();
            }

            var library = await _libraryService.GetLibraryByIdAsync(libraryId.Value);

            if (library == null)
            {
                return NotFound();
            }

            var readers = await _readerService.GetReadersByLibraryAsync(library.Id);
            ViewData["Title"] = $"Readers in '{library.Name}' library";
            ViewBag.CurrentLibraryId = library.Id;

            return View(readers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _readerService.GetReaderByIdAsync(id.Value);

            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        public async Task<IActionResult> Create(int? libraryId)
        {
            if (!libraryId.HasValue)
            {
                return NotFound();
            }

            var library = await _libraryService.GetLibraryByIdAsync(libraryId.Value);

            if (library == null)
            {
                return NotFound();
            }

            return View(new Reader { LibraryId = libraryId.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReader([Bind("Id,FirstName,LastName,Email,LibraryId")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                await _readerService.CreateReaderAsync(reader);
                TempData["ReaderSuccessMessage"] = "Reader created successfully!";
                return RedirectToAction(nameof(Index), new { libraryId = reader.LibraryId });
            }

            return View(reader);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _readerService.GetReaderByIdAsync(id.Value);

            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReader([Bind("Id,FirstName,LastName,Email,LibraryId")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _readerService.UpdateReaderAsync(reader);
                    TempData["ReaderSuccessMessage"] = "Reader updated successfully!";
                }
                catch (DataException)
                {
                    TempData["ReaderErrorMessage"] = "Unable to save changes.";
                }
                return RedirectToAction(nameof(Index), new { libraryId = reader.LibraryId });
            }

            return View(reader);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _readerService.GetReaderByIdAsync(id.Value);

            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReader(int id, int libraryId)
        {
            try
            {
                await _readerService.DeleteReaderAsync(id);
                TempData["ReaderSuccessMessage"] = "Reader deleted successfully!";
            }
            catch (KeyNotFoundException exception)
            {
                TempData["ReaderErrorMessage"] = exception;
            }
            catch (InvalidOperationException exception)
            {
                TempData["ReaderErrorMessage"] = exception;
            }

            return RedirectToAction(nameof(Index), new { libraryId });
        }
    }
}
