using AutoMapper;
using LibraryManagement.API.Data;
using LibraryManagement.API.DTOs.Borrow;
using LibraryManagement.API.Models;
using LibraryManagement.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowRepository _borrowRepo;
        private readonly IMapper _mapper;

        public BorrowController(IBorrowRepository borrowRepo, IMapper mapper)
        {
            _borrowRepo = borrowRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowReadDto>>> GetAll()
        {
            var borrows = await _borrowRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BorrowReadDto>>(borrows));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowReadDto>> GetById(int id)
        {
            var borrow = await _borrowRepo.GetByIdAsync(id);
            if (borrow == null) return NotFound();
            return Ok(_mapper.Map<BorrowReadDto>(borrow));
        }

        [HttpPost]
        public async Task<ActionResult<BorrowReadDto>> Create(BorrowCreateDto dto)
        {
            var borrow = _mapper.Map<Borrow>(dto);
            await _borrowRepo.AddAsync(borrow);
            await _borrowRepo.SaveChangesAsync();

            var readDto = _mapper.Map<BorrowReadDto>(borrow);
            return CreatedAtAction(nameof(GetById), new { id = borrow.Id }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BorrowUpdateDto dto)
        {
            var borrow = await _borrowRepo.GetByIdAsync(id);
            if (borrow == null) return NotFound();

            _mapper.Map(dto, borrow);
            await _borrowRepo.UpdateAsync(borrow);
            await _borrowRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrow = await _borrowRepo.GetByIdAsync(id);
            if (borrow == null) return NotFound();

            await _borrowRepo.DeleteAsync(borrow);
            await _borrowRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
