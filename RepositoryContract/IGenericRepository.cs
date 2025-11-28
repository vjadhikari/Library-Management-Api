namespace RepositoryContract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T book);
        Task UpdateAsync(T book);
        Task DeleteAsync(T book);
    }
}
