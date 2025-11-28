using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContract.DTOs.Member;
using ServiceContract.Interfaces;
using Services;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll()
        {
            List<Member>? members = null;
            try
            {
                members = await _memberService.GetAllMembers();
            }
            catch (Exception ex)
            {
                members = new List<Member>();
            }
            return Ok(_mapper.Map<IEnumerable<MemberDto>>(members));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetById(int id)
        {
            Member? member = null;
            try
            {
                member = await _memberService.GetMemberById(id);
                if (member == null) return NotFound();
            }
            catch (Exception)
            {
                member = new Member();
            }
            return Ok(_mapper.Map<MemberDto>(member));
        }

        [HttpPost]
        public async Task<ActionResult<MemberDto>> Create(CreateMemberDto createMemberDto)
        {
            Member? member = null;
            try
            {
                member = _mapper.Map<Member>(createMemberDto);
                await _memberService.AddMember(member);
            }
            catch (Exception)
            {
                member = new Member();
            }
            return CreatedAtAction(nameof(GetById), new { id = member.Id }, _mapper.Map<MemberDto>(member));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMemberDto updateMemberDto)
        {
            try
            {
                Member? member = new Member();
                _mapper.Map(updateMemberDto, member);
                if (await _memberService.UpdateMember(id, member))
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _memberService.DeleteMember(id))
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

            }
            return NotFound();
        }
    }
}
