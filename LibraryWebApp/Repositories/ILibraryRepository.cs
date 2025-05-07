using LibraryWebApp.Models;

namespace LibraryWebApp.Repositories
{
    public interface ILibraryRepository : IRepository<Library>
    {
        Task<Library?> GetByIdWithBooksAndReadersAsync(int id);
    }
}
