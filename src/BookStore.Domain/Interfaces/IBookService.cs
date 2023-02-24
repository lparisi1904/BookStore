using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IBookService: IDisposable //IDisposable: per il rilascio della memoria
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(long id);
        Task<Book> Add(Book book);
        Task<Book> Update(Book book);
        Task<Book> Remove(Book book);
        Task<IEnumerable<Book>> GetBooksByCategory(long categoryId);
        Task<IEnumerable<Book>> Search(string bookName);
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue);
    }
}
