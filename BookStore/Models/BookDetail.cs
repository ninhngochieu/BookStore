using System;

namespace BookStore.Models
{
    public class BookDetail
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string DistributorId { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public int Pages { get; set; }
        public string SKU { get; set; }
        public string PublisherId { get; set; }
        public string Description { get; set; }
    }
}
