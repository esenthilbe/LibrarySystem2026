namespace LibrarySystem2026.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ICollection<Book>? Books { get; set; }
        public ICollection<Member>? Members { get; set; }
    }
}
