using System.Linq.Expressions;
using BookStore.Domain.Common;


namespace BookStore.Domain.Interfaces
{
    // NOTE:
    // IEnumerable => piu veloce rispetto a List perché ottiene i dati solo quando sono necessari.
    // IDisposable => per rilasciare risorse non gestite. 

    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(long id);

        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);

        //passo un'espressione lambda per cercare 'qualsiasi' entità con 'qualsiasi' parametro.
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        
        Task<int> SaveChanges();
    }
}