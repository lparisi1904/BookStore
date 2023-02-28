using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Book
{
    public record BookResultDto
    {
        [Key]
        public long Id { get; init; }

        public long CategoryId { get; init; }
        public long PublisherId { get; init; }

        public string CategoryName { get; init; }

        public string Title { get; init; }

        public string Author { get; init; }
        public int YearBook { get; init; } 

        public string Description { get; init; }
    }
}
