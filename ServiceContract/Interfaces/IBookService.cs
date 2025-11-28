using Entities.Models;

namespace ServiceContract.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task AddBook(Book book);
        Task<bool> UpdateBook(int id, Book book);
        Task<bool> DeleteBook(int id);
    }
}
