using LibraryWebApp.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILibraryService _libraryService;

        public BookController(IBookService bookService, ILibraryService libraryService)
        {
            _bookService = bookService;
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
            
            var books = await _bookService.GetBooksByLibraryAsync(library.Id);
            ViewData["Title"] = $"Books in '{library.Name}' library";
            ViewBag.CurrentLibraryId = library.Id;
            
            return View(books);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookByIdAsync(id.Value);
            
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
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
            
            return View(new Book { LibraryId = libraryId.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook([Bind("Id,Title,Author,PublicationYear,LibraryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.CreateBookAsync(book);
                TempData["BookSuccessMessage"] = "Book created successfully!";
                return RedirectToAction(nameof(Index), new { libraryId = book.LibraryId });
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookByIdAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook([Bind("Id,Title,Author,PublicationYear,LibraryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBookAsync(book);
                    TempData["BookSuccessMessage"] = "Book updated successfully!";
                }
                catch (DataException)
                {
                    TempData["BookErrorMessage"] = "Unable to save changes.";
                }
                return RedirectToAction(nameof(Index), new { libraryId = book.LibraryId });
            }

            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookByIdAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(int id, int libraryId)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                TempData["BookSuccessMessage"] = "Book deleted successfully!";
            }
            catch (KeyNotFoundException exception)
            {
                TempData["BookErrorMessage"] = exception;
            }
            catch (InvalidOperationException exception)
            {
                TempData["BookErrorMessage"] = exception;
            }

            return RedirectToAction(nameof(Index), new { libraryId });
        }
    }
}
