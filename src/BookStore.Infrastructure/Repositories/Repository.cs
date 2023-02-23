using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace BookStore.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        // La proprietà Db è protetta perché tutte le classi che ereditano dal repository possono accedere alla proprietà Db.
        protected readonly BookStoreContext Db;
        // È possibile usare un oggetto DbSet<TEntity> per eseguire query e salvare le istanze di TEntity.
        // Le query LINQ su un DbSet< TEntity > verranno tradotte in query su DB.
        // La proprietà DbSet viene utilizzata come scorciatoia per eseguire le operazioni nel database
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(BookStoreContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        //AsNoTracking => leggendo qualcosa nel database, possiamo usare AsNoTracking per aumentare le prestazioni nel nostro applicazione.
        // nella scrittura / aggornamento NON usare
        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
