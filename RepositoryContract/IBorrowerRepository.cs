using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContract
{
    public interface IBorrowerRepository
    {
        Task<List<Borrow>> GetAllBorrowAsync();
        Task<Borrow?> GetBorrowByIdAsync(int id);
    }
}
