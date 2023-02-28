using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Book = BookStore.Domain.Entities.Book;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreContext context) : base(context) { }

        //AsNoTracking: =>
        //Se non è necessario aggiornare le entità recuperate dal database (readonly), deve essere usata una query con AsNoTracking
        public override async Task<List<Book>> GetAll() 
            => await Db.Books
                .AsNoTracking()
                .Include(c => c.Category)
                .OrderBy(c => c.Title)
                .ToListAsync();

        public override async Task<Book> GetById(long id) 
            => await Db.Books
                .AsNoTracking()
                .Include(b => b.Category)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Book>> GetBooksByCategory(long categoryId)
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
    }
}
