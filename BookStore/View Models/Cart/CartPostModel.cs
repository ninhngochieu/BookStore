using System;
namespace BookStore.ViewModels.Cart
{
    public class CartPostModel
    {
        public CartPostModel()
        {
        }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
    }
}
