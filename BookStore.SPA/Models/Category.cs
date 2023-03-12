using System.ComponentModel.DataAnnotations;

namespace BookStore.SPA.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo {0} è obbligatorio")]
        [StringLength(150, ErrorMessage = "Il campo {0} deve essere compreso tra i caratteri {2} e {1}.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
