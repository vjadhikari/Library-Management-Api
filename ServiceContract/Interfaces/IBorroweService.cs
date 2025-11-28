using Entities.Models;

namespace ServiceContract.Interfaces
{
    public interface IBorroweService
    {
        Task<List<Borrow>> GetAllBorrower();
        Task<Borrow?> GetBorrowerById(int id);
        Task AddBorrower(Borrow borrow);
        Task<bool> UpdateBorrower(int id, Borrow borrow);
        Task<bool> DeleteBorrower(int id);
    }
}
