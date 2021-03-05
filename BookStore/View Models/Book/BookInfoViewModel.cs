using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.View_Models.Book
{
    public class BookInfoViewModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime PublicationDate { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public int CategoryId{ get; set; }
        public ICollection<BookImage> Images{ get; set; }

    }
}
