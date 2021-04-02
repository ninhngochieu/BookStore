using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookStore.ViewModels.Cart;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly CartServices _cartServices;
        private readonly IMapper _mapper;

        public CartController(bookstoreContext context, CartServices cartServices, IMapper mapper)
        {
            _context = context;
            _cartServices = cartServices;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCartByUser(int id)
        {
            var cart = await _context.Carts
               .Where(c => c.UserId == id)
               .Include(u => u.User)
               .Include(b => b.Book)
               .ToListAsync();
            if (cart is null)
            {
                return NotFound(new {data = "Empty cart", success = true});
            }

            return Ok(new {data = _mapper.Map<List<CartViewModel>>(cart), success = true });
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(CartPostModel NewItem)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error_message = "Loi gio hang" });
            }
            Cart CurrentCart = await _cartServices.FindAsync(NewItem);
            if (CurrentCart is not null)
            {
                //Cap nhat lai gio hang
                CurrentCart.Amount += NewItem.Amount;
                if (await _cartServices.UpdateAsync(CurrentCart))
                {
                    return Ok(new { data = CurrentCart, success = true });
                }
                else
                {
                    return Ok(new { error_message = "Co loi khi cap nhat gio hang" });
                }
            }
            else
            {
                Cart cart = new Cart
                {
                    UserId = NewItem.UserId,
                    BookId = NewItem.BookId,
                    Amount = NewItem.Amount,
                };
                if (await _cartServices.AddNewCartAsync(cart))
                {
                    return Ok(new { data = cart, success = true });
                }
                else
                {
                    return Ok(new { error_message = "Loi them gio hang" });
                }
            }
            //return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            bool IsDelete = await _cartServices.DeleteCartAsync(id);
            if (IsDelete)
            {
                return Ok(new { data = "Xoa thanh cong", success = true });
            }
            else
            {
                return Ok(new { data = "Gio hang khong ton tai", success = true });
            }
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}