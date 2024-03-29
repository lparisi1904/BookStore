﻿using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookStore.API.Dtos.Book;
using Mapster;
using BookStore.API.Utils;
using static BookStore.API.Utils.ResponseMessageHttp;
using System.Net;
using Stripe;
using BookStore.Domain.Entities;

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
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAll(); 

            if (!books.Any() || books == null)
                return base.NotFound(ResponseMessageHttp.StatusCode.BookNotFound.GetDescription());

            var result = books.Adapt<IEnumerable<BookResultDto>>();

            return Ok(result);

        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBookById(long id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) { 
                return base.NotFound(ResponseMessageHttp.StatusCode.BookNotFound.GetDescription()); 
            };

            var result = book.Adapt<BookResultDto>();

            return Ok(result);
           // return Ok(StatusCode.CodeSuccess.GetDescription());
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBooksByCategory(long categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) { 
                return base.NotFound(ResponseMessageHttp.StatusCode.BookNotFound.GetDescription()); 
            }

            var result = books.Adapt<IEnumerable<BookResultDto>>();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddBook(BookAddDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var book = bookDto.Adapt(bookDto.Adapt<Book>());

            var bookAdded = await _bookService.Add(book);

            if (bookAdded == null) 
                return base.BadRequest(ResponseMessageHttp.StatusCode.CategorySuccessOK.GetDescription());
             

            return Ok(book.Adapt<BookResultDto>());
        }


        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateBook(long id, BookEditDto bookDto)
        {
            if (id != bookDto.Id) 
                return base.BadRequest(ResponseMessageHttp.StatusCode.BookNotMatch.GetDescription());

            if (!ModelState.IsValid) return BadRequest();

            await _bookService.Update(bookDto.Adapt<Book>());

            //return Ok(bookDto);
            return base.Ok(ResponseMessageHttp.StatusCode.BookSuccessUpdate.GetDescription());
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveBook(long id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return 
                    base.NotFound(ResponseMessageHttp.StatusCode.BookNotFound.GetDescription());

            await _bookService.Remove(book);

            return base.Ok(ResponseMessageHttp.StatusCode.BookSuccessDeleted.GetDescription());
        }

        [HttpGet]
        [Route("search/{bookTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Book>>> SearchBook(string bookTitle)
        {
            var book = await _bookService.Search(bookTitle);
            var books = book.Adapt<List<Book>>();

            if (books == null || !books.Any())  
                return base.NotFound(ResponseMessageHttp.StatusCode.BookNotFound.GetDescription());

            return Ok(books);
        }

        [HttpGet]
        [Route("search-book-with-category/{searchValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchValue)
        {
            var searchBook  = await _bookService.SearchBookWithCategory(searchValue);

            var books = searchBook.Adapt<List<Book>>();

            if (!books.Any())
                return base.NotFound(ResponseMessageHttp.StatusCode.BookNotFound.GetDescription());

            return Ok(books.Adapt<IEnumerable<BookResultDto>>());
        }
    }
}