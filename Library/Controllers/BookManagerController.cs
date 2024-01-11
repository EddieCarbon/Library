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
        var books = await _context.Books.ToListAsync();
        return View(books);
    }

    // GET: BookManager/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: BookManager/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Author,Publisher")] Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // POST: BookManager/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,Publisher")] Book book)
    {
        if (id != book.BookId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: BookManager/Delete/5
    public async Task<IActionResult> Delete(int? id)
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
        if (ModelState.IsValid)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
        if (ModelState.IsValid)
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(publisher);
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.BookId == id);
    }
}