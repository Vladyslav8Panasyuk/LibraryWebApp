using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetAllByLibraryAsync(int libraryId)
        {
            return await _context.Books
                .Where(book => book.LibraryId == libraryId)
                .Include(book => book.Library)
                .ToListAsync();
        }
    }
}
