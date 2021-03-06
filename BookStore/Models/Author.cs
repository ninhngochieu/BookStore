using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class Author
    {
        public Author()
        {
        }
        public int Id { get; set; }
        public string AuthorName { get; set; }

        public ICollection<Book> Books;
    }
}
