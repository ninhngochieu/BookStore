using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class Category
    {
        public Category()
        {
        }
        public int Id { get; set; }
        public string CategoryName { get; set; }

        ////Category - Book: 1 - n
        public ICollection<Book> Books { get; set; }
    }
}
