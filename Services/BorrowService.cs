using Entities.Models;
using RepositoryContract;
using ServiceContract.Interfaces;

namespace Services
{
    public class BorrowService : IBorroweService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowService(IUnitOfWork unitOfWork, IBorrowerRepository borrowerRepository)
        {
            _unitOfWork = unitOfWork;
            _borrowerRepository = borrowerRepository;
        }

        public async Task<List<Borrow>> GetAllBorrower()
        {
            return await _borrowerRepository.GetAllBorrowAsync();
        }

        public async Task<Borrow?> GetBorrowerById(int id)
        {
            return await _borrowerRepository.GetBorrowByIdAsync(id);
        }

        public async Task AddBorrower(Borrow borrow)
        {
            await _unitOfWork.BorrowRepository.AddAsync(borrow);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBorrower(int id, Borrow borrow)
        {
            var borrowDetail = await _borrowerRepository.GetBorrowByIdAsync(id);
            if (borrowDetail == null) return false;
            await _unitOfWork.BorrowRepository.UpdateAsync(borrow);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBorrower(int id)
        {
            var borrowDetail = await _borrowerRepository.GetBorrowByIdAsync(id);
            if (borrowDetail == null) return false;
            await _unitOfWork.BorrowRepository.DeleteAsync(borrowDetail);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
