using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Models;
using BookStore.API.Dtos.Book;
using Mapster;


namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();

            if (!books.Any() || books == null) return NotFound();

            var result = books.Adapt<IEnumerable<BookResultDto>>();

            return Ok(result);
        }


        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) { return NotFound(); }

            var result = book.Adapt<BookResultDto>();

            return Ok(result);
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksByCategory(long categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) { return NotFound(); }

            var result = books.Adapt<IEnumerable<BookResultDto>>();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(BookAddDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var book = bookDto.Adapt(bookDto.Adapt<Book>());

            var bookResult = await _bookService.Add(book);

            if (bookResult == null) return BadRequest();

            return Ok(book.Adapt<BookResultDto>());
        }


        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(long id, BookEditDto bookDto)
        {
            if (id != bookDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            await _bookService.Update(bookDto.Adapt<Book>());

            return Ok(bookDto);
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(long id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return NotFound("Libro non presente in archivio..");

            await _bookService.Remove(book);

            return Ok("Libro cancellato correttamente.");
        }

        [HttpGet]
        [Route("search/{bookTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> Search(string bookTitle)
        {
            var book = await _bookService.Search(bookTitle);
            var books = book.Adapt<List<Book>>();

            if (books == null || !books.Any())  
                return NotFound("Nessun libro è stato trovato.");

            return Ok(books);
        }

        [HttpGet]
        [Route("search-book-with-category/{searchedValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchValue)
        {
            var searchBook  = await _bookService.SearchBookWithCategory(searchValue);

            var books = searchBook.Adapt<List<Book>>();

            if (!books.Any()) 
                return NotFound("Nessun libro trovato");

            return Ok(books.Adapt<IEnumerable<BookResultDto>>());
        }
    }
}