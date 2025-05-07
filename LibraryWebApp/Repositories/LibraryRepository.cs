using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Repositories
{
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(ApplicationDbContext context) : base(context) { }
        
        public async Task<Library?> GetByIdWithBooksAndReadersAsync(int id)
        {
            return await _context.Libraries
                .Include(library => library.Books)
                .Include(library => library.Readers)
                .FirstOrDefaultAsync(library => library.Id == id);
        }
    }
}
