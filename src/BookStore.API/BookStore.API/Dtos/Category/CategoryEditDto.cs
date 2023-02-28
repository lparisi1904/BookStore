using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Category
{
    public record CategoryEditDto
    {
        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        public int Id { get; init; }


        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(50, ErrorMessage ="Il campo {0} deve essere tra {2} e {1} caratteri", MinimumLength =2)]
        public string Name { get; init; }
    }
}
