using AutoMapper;
using LibraryManagement.API.DTOs.Member;
using LibraryManagement.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Member = LibraryManagement.API.Models.Member;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberController(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll()
        {
            var members = await _memberRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MemberDto>>(members));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetById(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(_mapper.Map<MemberDto>(member));
        }

        [HttpPost]
        public async Task<ActionResult<MemberDto>> Create(CreateMemberDto createMemberDto)
        {
            var member = _mapper.Map<Member>(createMemberDto);
            await _memberRepository.AddAsync(member);
            return CreatedAtAction(nameof(GetById), new { id = member.Id }, _mapper.Map<MemberDto>(member));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMemberDto updateMemberDto)
        {
            if (!await _memberRepository.ExistsAsync(id)) return NotFound();

            var member = await _memberRepository.GetByIdAsync(id);
            _mapper.Map(updateMemberDto, member);

            await _memberRepository.UpdateAsync(member);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) return NotFound();

            await _memberRepository.DeleteAsync(member);
            return NoContent();
        }
    }
}
