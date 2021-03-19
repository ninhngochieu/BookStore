using System;
using Microsoft.AspNetCore.Http;

namespace BookStore.View_Models.Book
{
    public class UpdateBookPostModel
    {
        public UpdateBookPostModel()
        {
        }

        public int Id { get; set; }
#nullable enable
        public string? BookName { get; set; }
        
        public int? Price { get; set; }
        
        public int? Quantity { get; set; }
        public DateTime? PublicationDate { get; set; }
                
        public string? Description { get; set; }
        
        public IFormFile? MainImage { get; set; }
        
        public int? CategoryId { get; set; }
        
        public int? AuthorId { get; set; }
        public bool? Private { get; set; }

        public IFormFile Image1 { get; set; }
        public IFormFile? Image2 { get; set; }
        public IFormFile? Image3 { get; set; }
        public IFormFile? Image4 { get; set; }
#nullable disable

    }
}
