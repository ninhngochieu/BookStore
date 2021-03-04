using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public int Price{ get; set; }
        public int RatingCount{ get; set; }
        public double AverageRating { get; set; }
        public int Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? PublicationDate{ get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Description { get; set; }

        //Category - Book: 1-1
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}