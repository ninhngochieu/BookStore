using System;
using BookStore.Models;

namespace BookStore.ViewModels.CartViewModel
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
