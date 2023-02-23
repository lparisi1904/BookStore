using System.Linq.Expressions;
using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task Add(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(long id);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        //IEnumerable => piu veloce rispetto a List perché ottiene i dati solo quando sono necessari.
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}