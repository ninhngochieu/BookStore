using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.ViewModels.Cart;
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
            _bookstoreContext.Entry(getCartByUserId).State = EntityState.Modified; ;
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

        internal async Task<IList<Cart>> GetCartFromUser(int userId)
        {
            return await _bookstoreContext.Carts
                .Include(c => c.Book)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        internal async Task<bool> DeleteCartAsync(int id)
        {
            var cart = await _bookstoreContext.Carts.Where(x => x.UserId == id).ToListAsync();
            if (cart is null)
            {
                return false;
            }

            _bookstoreContext.Carts.RemoveRange(cart);
            return await _bookstoreContext.SaveChangesAsync() != 0;
        }

        internal async Task<object> GetCartByIdAsync(int id)
        {
            return await _bookstoreContext.Carts.FindAsync(id);
        }

        internal Task<List<Cart>> DeleteCartById(Cart currentCart)
        {
            _bookstoreContext.Remove(currentCart);
            _bookstoreContext.SaveChangesAsync();
            return _bookstoreContext.Carts
             .Where(u => u.UserId == currentCart.UserId)
             .Include(b => b.Book)
             .Include(u => u.User)
             .ToListAsync();
        }

        internal Task<List<Cart>> DeleteCartByCartId(int id)
        {
            Cart cart = _bookstoreContext.Carts.Find(id);
            _ = _bookstoreContext.Carts.Remove(cart);
            _ = _bookstoreContext.SaveChangesAsync();
            return _bookstoreContext.Carts
             .Where(u => u.UserId == cart.UserId)
             .Include(b => b.Book)
             .Include(u => u.User)
             .ToListAsync();
        }

        internal async Task DeleteCartByUserIdAsync(int userId)
        {
            List<Cart> carts = await _bookstoreContext.Carts.Where(u => u.UserId == userId).ToListAsync();
            _bookstoreContext.RemoveRange(carts);
            _ = await _bookstoreContext.SaveChangesAsync();
        }
    }
}