using System.ComponentModel.DataAnnotations;

namespace BookStore.SPA.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo Titolo è obbligatorio")]
        [StringLength(150, ErrorMessage = "Il campo {0} deve essere compreso tra i caratteri {2} e {1}.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Il campo Autore è obbligatorio")]
        [StringLength(150, ErrorMessage = "Il campo {0} deve essere compreso tra i caratteri {2} e {1}.", MinimumLength = 2)]
        public string Author { get; set; }

        public string Description { get; set; }

        public int? YearBook { get; set; }

        [Required(ErrorMessage = "Il campo Categoria è obbligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Il campo Categoria è obbligatorio")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
