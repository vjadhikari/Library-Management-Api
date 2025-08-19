using AutoMapper;
using LibraryManagement.API.DTOs.Book;
using LibraryManagement.API.Models;
using LibraryManagement.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepository.GetAllAsync();
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(booksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            await _bookRepository.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, _mapper.Map<BookDto>(book));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return NotFound();

            _mapper.Map(updateBookDto, book);
            await _bookRepository.UpdateAsync(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return NotFound();

            await _bookRepository.DeleteAsync(book);
            return NoContent();
        }
    }
}
