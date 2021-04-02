using BookStore.Models;

namespace BookStore.ViewModels.Cart
{
    public class CartViewModel
    {
        public CartViewModel()
        {
        }
        public int Id { get; set; }
        public int UserId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }
    }
}
