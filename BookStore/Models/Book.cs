using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
    }
}
