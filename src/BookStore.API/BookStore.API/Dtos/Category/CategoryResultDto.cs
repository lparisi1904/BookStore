using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Category
{
    public record CategoryResultDto
    {
        [Key]
        public int Id { get; init; }
        public string? Name { get; init; }
    }
}