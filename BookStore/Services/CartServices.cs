using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class CartServices : Service
    {
        public CartServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }


        internal async Task<bool> UpdateAsync(Cart getCartByUserId)
        {
            _bookstoreContext.Entry(getCartByUserId).State = EntityState.Modified;;
            return await _bookstoreContext.SaveChangesAsync() != 0;
        }

        internal async Task<bool> AddNewCartAsync(Cart cart)
        {
            _bookstoreContext.Carts.Add(cart);
            return await _bookstoreContext.SaveChangesAsync() != 0;
        }

        internal async Task<Cart> FindAsync(CartPostModel newItem)
        {
            return await _bookstoreContext.Carts
                .Where(c => c.UserId == newItem.UserId)
                .Where(c => c.BookId == newItem.BookId)
                .FirstOrDefaultAsync();
        }
    }
}
