using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContract
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllMemberAsync();
        Task<Member?> GetMemberByIdAsync(int id);
    }
}
