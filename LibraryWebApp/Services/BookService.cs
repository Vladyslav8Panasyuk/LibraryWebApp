using LibraryWebApp.Models;
using LibraryWebApp.Repositories;

namespace LibraryWebApp.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBooksByLibraryAsync(int libraryId)
        {
            return await _bookRepository.GetAllByLibraryAsync(libraryId);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task CreateBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            _bookRepository.Remove(book);
            await _bookRepository.SaveChangesAsync();
        }
    }
}
