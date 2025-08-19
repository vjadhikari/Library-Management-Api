using AutoMapper;
using LibraryManagement.API.Data;
using LibraryManagement.API.DTOs.Author;
using LibraryManagement.API.Models;
using LibraryManagement.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor(AuthorCreateDto createDto)
        {
            var author = _mapper.Map<Author>(createDto);
            await _repository.AddAsync(author);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, _mapper.Map<AuthorDto>(author));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, UpdateAuthorDto updateDto)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return NotFound();

            _mapper.Map(updateDto, author);
            await _repository.UpdateAsync(author);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return NotFound();

            await _repository.DeleteAsync(author);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
