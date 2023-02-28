using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Book
{
    public record class BookResultDto
    {
        [Key]
        public long Id { get; set; }

        public long CategoryId { get; set; }
        public long PublisherId { get; set; }
        
        public string CategoryName { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
    }
}
