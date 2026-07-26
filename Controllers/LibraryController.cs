using Microsoft.AspNetCore.Mvc;
using LibrarySystem2026.Data;
using LibrarySystem2026.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LibraryController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/libraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Library>>> GetLibraries()
        {
            return await _context.Libraries.Include(l => l.Books).Include(l => l.Members).ToListAsync();
        }

        // GET: api/libraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Library>> GetLibrary(int id)
        {
            var library = await _context.Libraries
                .Include(l => l.Books)
                .Include(l => l.Members)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (library == null) return NotFound();
            return library;
        }

        // POST: api/libraries
        [HttpPost]
        public async Task<ActionResult<Library>> AddLibrary(Library library)
        {
            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLibrary), new { id = library.Id }, library);
        }

        // PUT: api/libraries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibrary(int id, Library library)
        {
            if (id != library.Id) return BadRequest();
            _context.Entry(library).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/libraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            var library = await _context.Libraries.FindAsync(id);
            if (library == null) return NotFound();
            _context.Libraries.Remove(library);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
