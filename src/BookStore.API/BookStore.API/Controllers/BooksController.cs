//using AutoMapper;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using BookStore.Domain.Models;
using BookStore.API.Dtos.Book;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        //private readonly IMapper _mapper;


        public BooksController(IBookService bookService)
        {
            //_mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            //return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));

            var books = await _bookService.GetAll();

            var result = books.Adapt<IEnumerable<BookResultDto>>();

            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            //var book = await _bookService.GetById(id);

            //if (book == null) return NotFound();

            //return Ok(_mapper.Map<BookResultDto>(book));


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
            //var books = await _bookService.GetBooksByCategory(categoryId);

            //if (!books.Any()) return NotFound();

            //return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));

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

            return Ok(book);

            //if (!ModelState.IsValid) return BadRequest();

            //var book = _mapper.Map<Book>(bookDto);
            //var bookResult = await _bookService.Add(book);

            //if (bookResult == null) return BadRequest();

            //return Ok(_mapper.Map<Dtos.BookResultDto>(bookResult));
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(long id, BookEditDto bookDto)
        {
            //if (id != bookDto.Id) return BadRequest();

            //if (!ModelState.IsValid) return BadRequest();

            //await _bookService.Update(_mapper.Map<Book>(bookDto));

            //return Ok(bookDto);

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
            if (book == null) return NotFound();

            await _bookService.Remove(book);

            return Ok();
        }

        [HttpGet]
        [Route("search/{bookTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> Search(string bookName)
        {
            //var books = _mapper.Map<List<Book>>(await _bookService.Search(bookName));

            var book = await _bookService.Search(bookName);
            var books = book.Adapt<List<Book>>();

           // if (books == null || books.Count == 0) return NotFound("Nessun libro è stato trovato.");

            return Ok(books);
        }

        [HttpGet]
        [Route("search-book-with-category/{searchedValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchedValue)
        {
            //var books = _mapper.Map<List<Book>>(await _bookService.SearchBookWithCategory(searchedValue));

            //if (!books.Any()) return NotFound("None book was founded");

            //return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));

            var search  = await _bookService.SearchBookWithCategory(searchedValue);
            var books = search.Adapt<List<Book>>();

            if (!books.Any()) return NotFound("Nessun libro trovato");

            return Ok(books.Adapt<IEnumerable<BookResultDto>>());

        }


    }
}
