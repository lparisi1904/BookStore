﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Per la classe BookRepository non utilizzeremo GetById e GetAll della classe GENERICA, perché vogliamo restituire la categoria del libro in quei metodi. 
        /// se utilizziamo i metodi della classe Repository generica, non porterà il nome della categoria, 
        ///quindi avremo un GetAll e GetById specifici per questa classe.
        /// </summary>
        /// <returns></returns>
        new Task<List<Book>> GetAll();
        new Task<Book> GetById(int id);
        Task<IEnumerable<Book>> GetBooksByCategory(int categoryId);
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue);
    }
}