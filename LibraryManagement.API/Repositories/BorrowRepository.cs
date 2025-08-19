using LibraryManagement.API.Data;
using LibraryManagement.API.Models;
using LibraryManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly AppDbContext _context;

        public BorrowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrow>> GetAllAsync()
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Member)
                .ToListAsync();
        }

        public async Task<Borrow?> GetByIdAsync(int id)
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Borrow> AddAsync(Borrow borrow)
        {
            await _context.Borrows.AddAsync(borrow);
            return borrow;
        }

        public async Task UpdateAsync(Borrow borrow)
        {
            _context.Borrows.Update(borrow);
        }

        public async Task DeleteAsync(Borrow borrow)
        {
            _context.Borrows.Remove(borrow);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
