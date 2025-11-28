using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContract.DTOs.Borrow;
using ServiceContract.Interfaces;
using Services;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorroweService _borroweService;
        private readonly IMapper _mapper;

        public BorrowController(IBorroweService borroweService, IMapper mapper)
        {
            _borroweService = borroweService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowReadDto>>> GetAll()
        {
            List<Borrow>? borrows = null;
            try
            {
                borrows = await _borroweService.GetAllBorrower();
            }
            catch (Exception ex)
            {
                borrows = new List<Borrow>();
            }
            return Ok(_mapper.Map<IEnumerable<BorrowReadDto>>(borrows));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowReadDto>> GetById(int id)
        {
            Borrow? borrow = null;
            try
            {
                borrow = await _borroweService.GetBorrowerById(id);
                if (borrow == null) return NotFound();
            }
            catch (Exception)
            {
                borrow = new Borrow();
            }
            return Ok(_mapper.Map<BorrowReadDto>(borrow));
        }

        [HttpPost]
        public async Task<ActionResult<BorrowReadDto>> Create(BorrowCreateDto dto)
        {
            Borrow? borrow = null;
            try
            {
                borrow = _mapper.Map<Borrow>(dto);
                await _borroweService.AddBorrower(borrow);
            }
            catch (Exception)
            {
                borrow = new Borrow();
            }
            return CreatedAtAction(nameof(GetById), new { id = borrow.Id }, _mapper.Map<BorrowCreateDto>(borrow));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BorrowUpdateDto dto)
        {
            try
            {
                Borrow? borrow = new Borrow();
                _mapper.Map(dto, borrow);
                if (await _borroweService.UpdateBorrower(id, borrow))
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
                if (await _borroweService.DeleteBorrower(id))
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
