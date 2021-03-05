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
        public int AuthorName { get; set; }

        ICollection<Book> Books;
    }
}
