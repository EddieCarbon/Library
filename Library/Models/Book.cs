namespace Library.Models;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }

    // Navigation Properties
    public Author Author { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<Loan> Loans { get; set; }
}