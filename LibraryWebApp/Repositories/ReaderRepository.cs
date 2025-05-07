using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Repositories
{
    public class ReaderRepository : BaseRepository<Reader>, IReaderRepository
    {
        public ReaderRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Reader>> GetAllByLibraryAsync(int libraryId)
        {
            return await _context.Readers
                .Where(reader => reader.LibraryId == libraryId)
                .Include(reader => reader.Library)
                .ToListAsync();
        }
    }
}
