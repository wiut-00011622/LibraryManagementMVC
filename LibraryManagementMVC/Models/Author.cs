using System.Collections.Generic;

namespace LibraryManagementMVC.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}