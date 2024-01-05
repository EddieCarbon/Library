namespace Library.Models;

public class Reader
{
    public int ReaderId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Navigation Properties
    public ICollection<Loan> Loans { get; set; }
}