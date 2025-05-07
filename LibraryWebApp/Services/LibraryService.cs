using LibraryWebApp.Models;
using LibraryWebApp.Repositories;

namespace LibraryWebApp.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibraryService(ILibraryRepository libraryRepository) 
        {
            _libraryRepository = libraryRepository;    
        }

        public async Task<IEnumerable<Library>> GetLibrariesAsync()
        {
            return await _libraryRepository.GetAllAsync();
        }

        public async Task<Library?> GetLibraryByIdWithBooksAndReadersAsync(int id)
        {
            return await _libraryRepository.GetByIdWithBooksAndReadersAsync(id);
        }

        public async Task CreateLibraryAsync(Library library)
        {
            await _libraryRepository.AddAsync(library);
            await _libraryRepository.SaveChangesAsync();
        }

        public async Task UpdateLibraryAsync(Library library)
        {
            var existingLibrary = await _libraryRepository.GetByIdAsync(library.Id);
            if (existingLibrary == null)
            {
                throw new KeyNotFoundException($"Library with ID {library.Id} not found.");
            }
            _libraryRepository.Update(library);
            await _libraryRepository.SaveChangesAsync();
        }

        public async Task DeleteLibraryAsync(int id)
        {
            var library = await _libraryRepository.GetByIdWithBooksAndReadersAsync(id);
            
            if (library == null)
            {
                throw new KeyNotFoundException($"Library with ID {id} not found.");
            }

            if (library.Books.Any())
            {
                throw new InvalidOperationException("Cannot delete library: it has associated books.");
            }

            if (library.Readers.Any())
            {
                throw new InvalidOperationException("Cannot delete library: it has associated readers.");
            }

            _libraryRepository.Remove(library);
            await _libraryRepository.SaveChangesAsync();
        }
    }
}
