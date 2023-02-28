using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Category
{
    public record class CategoryResultDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
