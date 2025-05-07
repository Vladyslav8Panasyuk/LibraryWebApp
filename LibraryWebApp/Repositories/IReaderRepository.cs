using LibraryWebApp.Models;

namespace LibraryWebApp.Repositories
{
    public interface IReaderRepository : IRepository<Reader>
    {
        Task<IEnumerable<Reader>> GetAllByLibraryAsync(int libraryId);
    }
}
