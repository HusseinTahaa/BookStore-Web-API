namespace BookStoreAPI.DTOs.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;

    }
}
