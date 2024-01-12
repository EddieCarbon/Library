using Library.Areas.Identity.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Pobierz wszystkie książki
        var books = await _context.Books.Include(b => b.Author).Include(b => b.Publisher).ToListAsync();

        // Sprawdź, czy użytkownik jest zalogowany
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            // Jeśli tak, pobierz listę wypożyczeń dla danego użytkownika
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var userLoans = await _context.Loans.Where(l => l.UserId == userId).Select(l => l.BookId).ToListAsync();

            // Zaznacz książki, które są wypożyczone przez danego użytkownika
            foreach (var book in books)
            {
                book.IsReservedByUser = userLoans.Contains(book.BookId);
            }
        }

        // Tylko dostępne książki
        var availableBooks = books.Where(book => !book.IsReservedByUser).ToList();

        return View(availableBooks);
    }
    
    // GET: Home/Reserve/5
    public async Task<IActionResult> Reserve(int? id)
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

        // Sprawdź, czy użytkownik jest zalogowany
        var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (userId == null)
        {
            // Przekieruj do strony logowania
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        // Sprawdź, czy książka jest już wypożyczona przez tego użytkownika
        var userLoan = await _context.Loans
            .Where(l => l.UserId == userId && l.BookId == id)
            .FirstOrDefaultAsync();

        if (userLoan != null)
        {
            // Książka jest już wypożyczona przez tego użytkownika
            // Tutaj można dodać obsługę tego przypadku, na przykład przekierowanie do strony z informacją.
            return RedirectToAction("Index");
        }

        // Zarezerwuj książkę dla użytkownika
        var newLoan = new Loan
        {
            UserId = userId,
            BookId = book.BookId,
            LoanDate = DateTime.Now,
            ReturnDate = DateTime.Now.AddDays(14) // Przykładowy czas wypożyczenia (14 dni)
        };

        _context.Loans.Add(newLoan);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    // GET: Home/MyLoans
    public async Task<IActionResult> MyLoans()
    {
        // Sprawdź, czy użytkownik jest zalogowany
        if (User.Identity != null && !User.Identity.IsAuthenticated)
        {
            // Jeśli nie jest zalogowany, przekieruj do strony logowania
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        // Pobierz identyfikator użytkownika
        var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        // Pobierz wypożyczone przez użytkownika książki
        var userLoans = await _context.Loans
            .Include(l => l.Book)
            .Where(l => l.UserId == userId)
            .ToListAsync();

        return View(userLoans);
    }
}