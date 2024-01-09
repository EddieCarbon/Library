namespace Library.Models;

public class Loan
{
    public int LoanId { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ReturnDate { get; set; }
}