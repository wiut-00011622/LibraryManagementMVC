namespace LibraryManagementMVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

        // Navigation properties
        public Author Author { get; set; }
        public Category Category { get; set; }
    }
}