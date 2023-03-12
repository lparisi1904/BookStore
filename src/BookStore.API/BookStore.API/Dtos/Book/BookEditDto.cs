using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Book
{
    public record BookEditDto
    {
        [Key]
        public long Id { get; init; }


        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        public long CategoryId { get; init; }


        [Required(ErrorMessage = "Il campo {0} è richiesto")]
        public long PublisherId { get; init; }


        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(100,ErrorMessage ="Il campo {0} deve essere tra {2} e {1} caratteri",MinimumLength =2)]
        public string? Title { get; init; }

        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(100,ErrorMessage = "Il campo {0} deve essere tra {2} e {1} caratteri", MinimumLength = 2)]
        
        
        public string? Author { get; init; }
        public int Year { get; init; }

        public string? Description { get; init; }
    }
}
