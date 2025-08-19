using LibraryManagement.API.Models;

namespace LibraryManagement.API.Repositories.Interfaces
{
    public interface IBorrowRepository
    {
        Task<IEnumerable<Borrow>> GetAllAsync();
        Task<Borrow?> GetByIdAsync(int id);
        Task<Borrow> AddAsync(Borrow borrow);
        Task UpdateAsync(Borrow borrow);
        Task DeleteAsync(Borrow borrow);
        Task<bool> SaveChangesAsync();
    }
}
