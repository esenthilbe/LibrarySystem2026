namespace LibrarySystem2026.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;

        // Foreign Key
        public int? LibraryId { get; set; } //nullable
        public Library? Library { get; set; }
    }
}
