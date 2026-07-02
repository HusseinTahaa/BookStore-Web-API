namespace BookStoreAPI.DTOs.Books
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}
