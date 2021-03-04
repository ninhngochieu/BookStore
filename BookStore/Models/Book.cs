using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string PublicationDate { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Description { get; set; }
        
        //Navigation Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        //
        public IList<BookImage> Images { get; set; }
        //
        public IList<BookComment> Comments { get; set; }
    }
}
