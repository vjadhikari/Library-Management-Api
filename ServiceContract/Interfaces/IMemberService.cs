using Entities.Models;

namespace ServiceContract.Interfaces
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllMembers();
        Task<Member?> GetMemberById(int id);
        Task AddMember(Member member);
        Task<bool> UpdateMember(int id,Member member);
        Task<bool> DeleteMember(int id);
    }
}
