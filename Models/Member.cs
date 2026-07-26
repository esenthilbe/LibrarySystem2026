namespace LibrarySystem2026.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Book>? BorrowedBooks { get; set; }
    }
}
