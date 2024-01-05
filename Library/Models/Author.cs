namespace Library.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation Properties
        public ICollection<Book> Books { get; set; }
    }
}