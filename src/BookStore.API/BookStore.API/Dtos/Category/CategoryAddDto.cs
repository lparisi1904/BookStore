using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Category
{
    public class CategoryAddDto
    {
        [Required(ErrorMessage ="Il campo {0} è richiesto")]
        [StringLength(150, ErrorMessage ="Il campo {0} deve essere tra {2} e {1} caratteri", MinimumLength =2)]
        public string Name { get; set; }
    }
}
