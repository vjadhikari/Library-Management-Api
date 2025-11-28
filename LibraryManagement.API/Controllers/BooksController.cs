using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceContract.DTOs.Book;
using ServiceContract.Interfaces;
using Services;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookRepository, IMapper mapper)
        {
            _bookService = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Book>? books = null;
            try
            {
                books = await _bookService.GetAllBooks();
            }
            catch (Exception ex) 
            {
                books = new List<Book>();
            }
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Book? book = null;
            try
            {
                book = await _bookService.GetBookById(id);
                if (book == null) return NotFound();
            }
            catch (Exception)
            {
                book = new Book();
            }
            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto createBookDto)
        {
            Book? book = null;
            try
            {
                book = _mapper.Map<Book>(createBookDto);
                await _bookService.AddBook(book);
            }
            catch (Exception)
            {
                book = new Book();
            }
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, _mapper.Map<CreateBookDto>(book));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBookDto updateBookDto)
        {
            try
            {
                Book? book = new Book();
                _mapper.Map(updateBookDto, book);
                if (await _bookService.UpdateBook(id, book))
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
                if (await _bookService.DeleteBook(id))
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
