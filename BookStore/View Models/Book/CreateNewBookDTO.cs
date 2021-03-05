using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookStore.View_Models.Book
{
    public class CreateNewBookDTO
    {
        public CreateNewBookDTO()
        {
            PublicationDate = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
        [Required]
        public string BookName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public long PublicationDate { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile MainImage { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public IFormFile Image1 { get; set; }
        public IFormFile? Image2 { get; set; }
        public IFormFile? Image3 { get; set; }
        public IFormFile? Image4 { get; set; }

    }
}
