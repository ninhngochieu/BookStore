using System;
using Microsoft.AspNetCore.Http;

namespace BookStore.ViewModels.Book
{
    public class UpdateBookPutModel
    {
        public UpdateBookPutModel()
        {
        }

        public int? id { get; set; }
        public string? BookName { get; set; }
        
        public int? Price { get; set; }
        
        public int? Quantity { get; set; }
        public DateTime? PublicationDate { get; set; }
        
        public string? SKU { get; set; }
        
        public string? Description { get; set; }
        
        public IFormFile? MainImage { get; set; }
        
        public int? CategoryId { get; set; }
        
        public int? AuthorId { get; set; }
        public bool? Private { get; set; }
    }
}
