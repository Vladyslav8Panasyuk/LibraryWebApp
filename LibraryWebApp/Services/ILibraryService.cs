using LibraryWebApp.Models;

namespace LibraryWebApp.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<Library>> GetLibrariesAsync();
        Task<Library?> GetLibraryByIdWithBooksAndReadersAsync(int id);
        Task CreateLibraryAsync(Library library);
        Task UpdateLibraryAsync(Library library);
        Task DeleteLibraryAsync(int id);
    }
}
