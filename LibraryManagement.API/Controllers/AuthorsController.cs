using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContract.DTOs.Author;
using ServiceContract.Interfaces;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            List<Author>? authors = null;
            try
            {
                authors = await _authorService.GetAllAuthor();
            }
            catch (Exception)
            { 
                authors = new List<Author>();
            }
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            Author? author = null;
            try
            {
                author = await _authorService.GetAuthorById(id);
                if (author == null) return NotFound();
            }
            catch(Exception) 
            {
                author=new Author();
            }
            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor(AuthorCreateDto createDto)
        {
            Author? author = null;
            try
            {
                author = _mapper.Map<Author>(createDto);
                await _authorService.AddAuthor(author);
            }
            catch (Exception)
            {
                author = new Author();
            }
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, _mapper.Map<AuthorDto>(author));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, UpdateAuthorDto updateDto)
        {
            try
            {
                Author? author = new Author();
                _mapper.Map(updateDto, author);
                if (await _authorService.UpdateAuthor(id, author))
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
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try
            {
                if (await _authorService.DeleteAuthor(id))
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
