using Library.Areas.Identity.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class BookManagerController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookManagerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: BookManager
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .ToListAsync();
        
        return View(books);
    }

    // GET: BookManager/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,AuthorId,PublisherId")] Book book)
    {
        book.Author = _context.Authors.Find(book.AuthorId);
        book.Publisher = _context.Publishers.Find(book.PublisherId);
        try {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
        }
        return View(book);
    }

    // GET: BookManager/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
        
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // POST: BookManager/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,AuthorId,PublisherId")] Book book)
    {
        if (id != book.BookId)
        {
            return NotFound();
        }

        try
        {
            // Pobierz oryginalną książkę z bazy danych
            var originalBook = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == book.BookId);

            if (originalBook == null)
            {
                return NotFound();
            }

            // Zaktualizuj właściwości książki na podstawie przekazanego modelu
            originalBook.Title = book.Title;
            originalBook.AuthorId = book.AuthorId;
            originalBook.PublisherId = book.PublisherId;

            // Zaktualizuj powiązane encje (Author i Publisher)
            _context.Update(originalBook);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(book.BookId))
            {
                return NotFound();
            }
            throw;
        }
        return RedirectToAction(nameof(Index));
    }


    // GET: BookManager/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(m => m.BookId == id);
        
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // POST: BookManager/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    // GET: BookManager/CreateAuthor
    public IActionResult CreateAuthor()
    {
        return View();
    }

    // POST: BookManager/CreateAuthor
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAuthor([Bind("FirstName,LastName")] Author author)
    {
        try
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
        }
        return View(author);
    }

    // GET: BookManager/CreatePublisher
    public IActionResult CreatePublisher()
    {
        return View();
    }

    // POST: BookManager/CreatePublisher
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePublisher([Bind("Name")] Publisher publisher)
    {
        try
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
        }
        return View(publisher);
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.BookId == id);
    }
}