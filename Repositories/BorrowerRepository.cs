using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly AppDbContext _context;
        public BorrowerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Borrow>> GetAllBorrowAsync()
        {
            return await _context.Borrows
               .Include(b => b.Book)
               .Include(b => b.Member)
               .ToListAsync();
        }

        public async Task<Borrow?> GetBorrowByIdAsync(int id)
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
