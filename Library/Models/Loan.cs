namespace Library.Models;

public class Loan
{
    public int LoanId { get; set; }
    public int BookId { get; set; }
    public int ReaderId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }

    // Navigation Properties
    public Book Book { get; set; }
    public Reader Reader { get; set; }
}