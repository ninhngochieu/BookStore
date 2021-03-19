using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookStore.View_Models.Book
{
    public class CreateNewBookDTO
    {
        public CreateNewBookDTO()
        {
            PublicationDate = DateTime.Now;
        }
        [Required]
        public string BookName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime? PublicationDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile MainImage { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public bool? Private{ get; set; }
        public IFormFile Image1 { get; set; }
#nullable enable
        public IFormFile? Image2 { get; set; }
        public IFormFile? Image3 { get; set; }
        public IFormFile? Image4 { get; set; }
#nullable disable

    }
}
