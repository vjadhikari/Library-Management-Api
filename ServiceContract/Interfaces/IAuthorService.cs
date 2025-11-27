using Entities.Models;

namespace ServiceContract.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthor();
        Task<Author?> GetAuthorById(int id);
        Task AddAuthor(Author author);
        Task<bool> UpdateAuthor(int id, Author author);
        Task<bool> DeleteAuthor(int id);
    }
}
