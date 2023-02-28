using BookStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Book
{
    public record BookAddDto
    {
        [Required (ErrorMessage ="il campo {0} è richiesto")]
        public long CategoryId { get; init; }

        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        public int PublisherId { get; init; }

        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(100, ErrorMessage ="il campo {0} deve essere tra {2} e {1} caratteri", MinimumLength =2)]
        public string Title { get; init; }

        [Required (ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(100, ErrorMessage ="Il campo {0} deve essere tra {2} e {1} caratteri", MinimumLength =2)]
        public string Author { get; init; }

        public int YearBook { get; init; }

        public string Description { get; init; }
            
    }
}

