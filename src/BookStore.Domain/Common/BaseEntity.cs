using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Common
{
    public abstract class BaseEntity
    {
        //public virtual Guid Id { get; set; }
        [Key]
        public long Id { get; set; }

        // public DateTime DateCreated { get; set; }
        //public DateTime? DateUpdated { get; set; }
        //public DateTime? DateDeleted { get; set; }
    }
}