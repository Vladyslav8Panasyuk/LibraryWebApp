using LibraryWebApp.Models;

namespace LibraryWebApp.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksByLibraryAsync(int libraryId);
        Task<Book?> GetBookByIdAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
