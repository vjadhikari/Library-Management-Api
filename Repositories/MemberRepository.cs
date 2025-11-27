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
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;
        public MemberRepository(AppDbContext context)
        {
            _context= context;
        }
        public async Task<List<Member>> GetAllMemberAsync()
        {
            return await _context.Members
                 .Include(m => m.Borrows)
                 .ToListAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _context.Members
                 .Include(m => m.Borrows)
                 .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
