using System.ComponentModel.DataAnnotations;

namespace BookStore.View_Models
{
    public class BookViewModel
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
