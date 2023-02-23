using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Models
{
    public abstract class BaseEntity
    {
        //public virtual Guid Id { get; set; }
        [Key]
        public long Id { get; set; }
    }
}