namespace BookStore.API.Dtos.Book
{
    public class BookResultDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
    }
}
