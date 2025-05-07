using LibraryWebApp.Models;

namespace LibraryWebApp.Services
{
    public interface IReaderService
    {
        Task<IEnumerable<Reader>> GetReadersByLibraryAsync(int libraryId);
        Task<Reader?> GetReaderByIdAsync(int id);
        Task CreateReaderAsync(Reader reader);
        Task UpdateReaderAsync(Reader reader);
        Task DeleteReaderAsync(int id);
    }
}
