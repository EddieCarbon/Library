namespace Library.Models;

public class Publisher
{
    public int PublisherId { get; set; }
    public string Name { get; set; }

    // Navigation Properties
    public ICollection<Book> Books { get; set; }
}