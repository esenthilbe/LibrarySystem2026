using Microsoft.EntityFrameworkCore;
using LibrarySystem2026.Models;
namespace LibrarySystem2026.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Library> Libraries { get; set; }
    }
}
