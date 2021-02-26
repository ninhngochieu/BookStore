using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.View_Models
{
    public class BookDetailViewModel
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Distributor { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public int Pages { get; set; }
        public string SKU { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
    }
}
