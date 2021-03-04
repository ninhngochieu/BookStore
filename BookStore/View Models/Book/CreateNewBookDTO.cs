using System.ComponentModel.DataAnnotations;

namespace BookStore.View_Models.Book
{
    public class CreateNewBookDTO
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public int Price { get; set; }
        public int RatingCount { get; set; }
        public double AverageRating { get; set; }
        public int Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? PublicationDate { get; set; }
        [Required]
        public string SKU { get; set; }
        
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
