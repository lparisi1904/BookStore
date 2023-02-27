using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Models;
using BookStore.API.Dtos.Book;
using Mapster;
using BookStore.API.Utils;
using static BookStore.API.Utils.Enums;

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

            if (!books.Any() || books == null) 
                return NotFound(MessageCode.BookNotFound.GetDescription());

            var result = books.Adapt<IEnumerable<BookResultDto>>();

            return Ok(result);
        }


        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) { 
                return NotFound(MessageCode.BookNotFound.GetDescription()); 
            };

            var result = book.Adapt<BookResultDto>();

            return Ok(result);
           // return Ok(MessageCode.CodeSuccess.GetDescription());
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksByCategory(long categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) { 
                return NotFound(MessageCode.BookNotFound.GetDescription()); 
            }

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

            var bookAdded = await _bookService.Add(book);

            if (bookAdded == null) 
                return BadRequest(MessageCode.CategorySuccessOK.GetDescription());
             

            return Ok(book.Adapt<BookResultDto>());
        }


        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(long id, BookEditDto bookDto)
        {
            if (id != bookDto.Id) 
                return BadRequest(MessageCode.BookNotMatch.GetDescription());

            if (!ModelState.IsValid) return BadRequest();

            await _bookService.Update(bookDto.Adapt<Book>());

            //return Ok(bookDto);
            return Ok(MessageCode.BookSuccessUpdate.GetDescription());
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(long id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return 
                    NotFound(MessageCode.BookNotFound.GetDescription());

            await _bookService.Remove(book);

            return Ok(MessageCode.BookSuccessDeleted.GetDescription());
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
                return NotFound(MessageCode.BookNotFound.GetDescription());

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
                return NotFound(MessageCode.BookNotFound.GetDescription());

            return Ok(books.Adapt<IEnumerable<BookResultDto>>());
        }
    }
}