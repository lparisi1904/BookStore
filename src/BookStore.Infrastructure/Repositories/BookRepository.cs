//using BookStore.Domain.Interfaces;
//using BookStore.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookStoreContext context) : base(context) { }


        //AsNoTracking: Se non è necessario aggiornare le entità recuperate dal database (readonly), deve essere usata una query con AsNoTracking
        public async Task<List<Book>> GetAll() 
            => await Db.Books
                .AsNoTracking()
                .Include(c => c.Category)
                .OrderBy(c => c.Title)
                .ToListAsync();

        public async Task<Book> GetById(int id) 
            => await Db.Books
                .AsNoTracking()
                .Include(b => b.Category)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await Search(b => b.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue)
        {
            return await Db.Books
                .AsNoTracking()
                .Include(c=> c.Category)
                .Where(b=> b.Title.Contains(searchValue) ||
                           b.Author.Contains(searchValue) ||
                           b.Description.Contains(searchValue) ||
                           b.Category.Name.Contains(searchValue))
                .ToListAsync();
        }



        public Task Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        
        public Task Remove(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
