﻿using System.Collections.Generic;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }

        public virtual IList<Book> Books { get; set; }
    }
}