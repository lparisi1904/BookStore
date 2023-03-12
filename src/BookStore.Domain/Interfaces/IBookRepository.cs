using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        /// <summary>
        /// Per la classe BookRepository non utilizzeremo GetById e GetAll della classe GENERICA, 
        /// perché vogliamo restituire la CATEGORIA del libro in quei metodi. 
        /// se utilizziamo i metodi della classe Repository generica, non porterà il nome della categoria, 
        ///quindi avremo un GetAll e GetById specifici per questa classe.
        /// </summary>
        /// <returns></returns>
        /// 
        // => Aggiungo i metodi personalizzati per Book
        new Task<List<Book>> GetAll();
        new Task<Book> GetById(long id);
        Task<IEnumerable<Book>> GetBooksByCategory(long categoryId);
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue);
    }
}