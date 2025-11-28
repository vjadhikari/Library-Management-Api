using Entities.Models;
using RepositoryContract;
using ServiceContract.Interfaces;

namespace Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }

        public async Task<List<Author>> GetAllAuthor() => await _unitOfWork.AuthorGetRepository.GetAllAsync();
        public async Task<Author?>GetAuthorById(int id) => await _unitOfWork.AuthorGetRepository.GetByIdAsync(id);
        
        public async Task AddAuthor(Author author)
        {
            await _unitOfWork.AuthorRepository.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateAuthor(int id,Author author)
        {
            var authorDetail = await _unitOfWork.AuthorGetRepository.GetByIdAsync(id);
            if (authorDetail == null) return false;
            await _unitOfWork.AuthorRepository.UpdateAsync(author);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var authorDetail = await _unitOfWork.AuthorGetRepository.GetByIdAsync(id);
            if (authorDetail == null) return false;
            await _unitOfWork.AuthorRepository.DeleteAsync(authorDetail);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
