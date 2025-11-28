using Entities.Models;
using RepositoryContract;
using ServiceContract.Interfaces;

namespace Services
{
    public class BookService: IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return  await _unitOfWork.BookGetRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _unitOfWork.BookGetRepository.GetByIdAsync(id);
        }

        public async Task AddBook(Book book)
        {

            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBook(int id, Book book)
        {
            var bookDetail = await _unitOfWork.BookGetRepository.GetByIdAsync(id);
            if (bookDetail == null) return false;
            await _unitOfWork.BookRepository.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var bookDetail = await _unitOfWork.BookGetRepository.GetByIdAsync(id);
            if (bookDetail == null) return false;
            await _unitOfWork.BookRepository.DeleteAsync(bookDetail);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
