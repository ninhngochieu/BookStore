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

        public IList<Book> Books { get; set; }
    }
}
