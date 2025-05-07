using LibraryWebApp.Models;

namespace LibraryWebApp.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllByLibraryAsync(int libraryId);
    }
}
