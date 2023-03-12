using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        //public virtual Guid Id { get; set; }
    }
}