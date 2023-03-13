using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
                .Include(cat => cat.Category)
                .OrderBy(cate => cate.Title)
                .ToListAsync();

        public override async Task<Book> GetById(long id) 
            => await Db.Books
                .AsNoTracking()
                .Include(cat => cat.Category)
                .Where(cate => cate.Id == id)
                .FirstOrDefaultAsync();


        /// <summary>
        /// Da Id di Categoria otteniamo una lista di libri appartenenti a quella Categoria (Id)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> GetBooksByCategory(long categoryId)
        {
            return await Search(b => b.CategoryId == categoryId);
        }


        /// <summary>
        /// Ricerca tutti i libri avendo come criterio ricerca il Titolo, l'Autore, 
        /// il nome della Categoria o la Descrizione
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns>
        /// Ritorna tutti i libri avendo come criterio ricerca 'searchValue'
        /// </returns>
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
