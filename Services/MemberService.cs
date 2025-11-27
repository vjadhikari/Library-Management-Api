using Entities.Models;
using RepositoryContract;
using ServiceContract.Interfaces;

namespace Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemberRepository _memberRepository;

        public MemberService(IUnitOfWork unitofwork, IMemberRepository memberRepository)
        {
            _unitOfWork = unitofwork;
            _memberRepository = memberRepository;
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _memberRepository.GetAllMemberAsync();
        }

        public async Task<Member?> GetMemberById(int id)
        {
            return await _memberRepository.GetMemberByIdAsync(id);
        }

        public async Task AddMember(Member member)
        {

            await _unitOfWork.MemberRepository.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateMember(int id,Member member)
        {
            var memberDetail = await _memberRepository.GetMemberByIdAsync(id);
            if (memberDetail == null) return false;
            await _unitOfWork.MemberRepository.UpdateAsync(member);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMember(int id)
        {
            var memberDetail = await _memberRepository.GetMemberByIdAsync(id);
            if (memberDetail == null) return false;
            await _unitOfWork.MemberRepository.DeleteAsync(memberDetail);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
