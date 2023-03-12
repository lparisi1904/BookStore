using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    // utilizzato nel Controller...
    //
    public interface IBookService: IDisposable //IDisposable: per il rilascio della memoria
    {
        /// <summary>
        /// Ricerca tutti il libri disponibili in archivio
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetAll();

        /// <summary>
        /// Ricerca un libro per codice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book> GetById(long id);
        
        Task<Book> Add(Book book);
        Task<Book> Update(Book book);
        Task<Book> Remove(Book book);
        
        
        /// <summary>
        /// Ricerca un libro per codice categoria
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksByCategory(long categoryId);

        
        /// <summary>
        /// Effettua una ricerca per titolo libro
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> Search(string bookTitle);
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue);
    }
}