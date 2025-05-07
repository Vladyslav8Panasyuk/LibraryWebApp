using LibraryWebApp.Models;
using LibraryWebApp.Repositories;

namespace LibraryWebApp.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;

        public ReaderService(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<IEnumerable<Reader>> GetReadersByLibraryAsync(int libraryId)
        {
            return await _readerRepository.GetAllByLibraryAsync(libraryId);
        }

        public async Task<Reader?> GetReaderByIdAsync(int id)
        {
            return await _readerRepository.GetByIdAsync(id);
        }

        public async Task CreateReaderAsync(Reader reader)
        {
            await _readerRepository.AddAsync(reader);
            await _readerRepository.SaveChangesAsync();
        }

        public async Task UpdateReaderAsync(Reader reader)
        {
            _readerRepository.Update(reader);
            await _readerRepository.SaveChangesAsync();
        }

        public async Task DeleteReaderAsync(int id)
        {
            var reader = await _readerRepository.GetByIdAsync(id);
            if (reader == null)
            {
                throw new KeyNotFoundException($"Reader with ID {id} not found.");
            }
            _readerRepository.Remove(reader);
            await _readerRepository.SaveChangesAsync();
        }
    }
}
