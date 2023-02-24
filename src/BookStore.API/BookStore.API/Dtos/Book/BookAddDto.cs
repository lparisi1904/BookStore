using BookStore.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Book
{
    public class BookAddDto
    {
        [Required (ErrorMessage ="il campo {0} è richiesto")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(100, ErrorMessage ="il campo {0} deve essere tra {2} e {1} caratteri", MinimumLength =2)]
        public string Title { get; set; }

        [Required (ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(100, ErrorMessage ="Il campo {0} deve essere tra {2} e {1} caratteri", MinimumLength =2)]
        public string Author { get; set; }

        public string Description { get; set; }
    }
}

