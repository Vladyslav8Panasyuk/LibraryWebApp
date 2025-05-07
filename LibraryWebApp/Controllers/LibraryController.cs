using LibraryWebApp.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryWebApp.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task<IActionResult> Index()
        {
            var libraries = await _libraryService.GetLibrariesAsync();
            return View(libraries);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _libraryService.GetLibraryByIdWithBooksAndReadersAsync(id.Value);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateLibrary([Bind("Id,Name,Address")] Library library)
        {
            if (ModelState.IsValid)
            {
                await _libraryService.CreateLibraryAsync(library);
                TempData["SuccessMessage"] = "Library created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(library);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _libraryService.GetLibraryByIdAsync(id.Value);
            
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLibrary([Bind("Id,Name,Address")] Library library)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _libraryService.UpdateLibraryAsync(library);
                    TempData["SuccessMessage"] = "Library updated successfully!";
                }
                catch (DataException)
                {
                    TempData["ErrorMessage"] = "Unable to save changes.";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(library);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _libraryService.GetLibraryByIdAsync(id.Value);

            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            try
            {
                await _libraryService.DeleteLibraryAsync(id);
                TempData["SuccessMessage"] = "Library deleted successfully!";
            }
            catch (KeyNotFoundException exception)
            {
                TempData["ErrorMessage"] = exception;
            }
            catch (InvalidOperationException exception)
            {
                TempData["ErrorMessage"] = exception;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
