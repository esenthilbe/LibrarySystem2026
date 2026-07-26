using Microsoft.AspNetCore.Mvc;
using LibrarySystem2026.Data;
using LibrarySystem2026.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;
        public BooksController(LibraryContext context)
        {
            _context = context;
        }
        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return book;
        }
        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // PUT: api/books/borrow/5?memberId=1
        [HttpPut("borrow/{id}")]
        public async Task<IActionResult> BorrowBook(int id, int memberId)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound("Book not found");
            if (!book.IsAvailable) return BadRequest("Book already borrowed");

            var member = await _context.Members.FindAsync(memberId);
            if (member == null) return NotFound("Member not found");

            book.IsAvailable = false;
            await _context.SaveChangesAsync();
            return Ok($"Book '{book.Title}' borrowed by {member.Name}");
        }

        // PUT: api/books/return/5
        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound("Book not found");

            book.IsAvailable = true;
            await _context.SaveChangesAsync();
            return Ok($"Book '{book.Title}' returned");
        }

    }
}
