using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public string MainImage { get; set; }
        public IList<string> SecondaryImages { get; set; }
        
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
