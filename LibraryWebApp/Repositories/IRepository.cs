namespace LibraryWebApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> SaveChangesAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
