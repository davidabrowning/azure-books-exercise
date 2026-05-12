using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public BooksController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // GET: api/books
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _appDbContext.Books.ToListAsync();
        return Ok(books);
    }

    // GET: api/books/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _appDbContext.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        _appDbContext.Books.Add(book);
        await _appDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }
}