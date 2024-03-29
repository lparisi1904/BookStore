﻿using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace BookStore.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;


        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetById(long id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<Book?> Add(Book book)
        {
            if (_bookRepository
                .Search(b => b.Title == book.Title)
                .Result.Any())
                 return null;

            await _bookRepository.Add(book);
            return book;
        }

        public async Task<Book?> Update(Book book)
        {
            if (_bookRepository.Search(b => b.Title == book.Title && b.Id != book.Id).Result.Any())
                return null;

            await _bookRepository.Update(book);
            return book;
        }
        public async Task<Book> Remove(Book book)
        {
            await _bookRepository.Remove(book);
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(long categoryId)
        {
            return await _bookRepository.GetBooksByCategory(categoryId);
        }

        public async Task<IEnumerable<Book>> Search(string bookTitle)
        {
            return await _bookRepository.Search(c => c.Title.Contains(bookTitle));
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue)
        {
            return await _bookRepository.SearchBookWithCategory(searchValue);
        }

        public void Dispose()
        {
            _bookRepository?.Dispose();
        }
    }
}
