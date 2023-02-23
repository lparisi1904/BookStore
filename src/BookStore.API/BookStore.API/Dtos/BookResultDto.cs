using BookStore.Domain.Models;

namespace BookStore.API.Dtos
{
    public record class BookResultDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? CategoryName { get; set; }

        public string? PublisherName { get; set; }

    }
}
