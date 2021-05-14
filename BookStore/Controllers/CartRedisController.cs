using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using StackExchange.Redis;
using BookStore.Modules;
using AutoMapper;
using BookStore.ViewModels.Cart;
using System;
using BookStore.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartRedisController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly CartServices _cartServices;
        private readonly BookServices _bookServices;
        private IDatabase database;
        private readonly IMapper _mapper;

        public CartRedisController(bookstoreContext context,
                                   IConnectionMultiplexer connectionMultiplexer,
                                   IMapper mapper,
                                   CartServices cartServices,
                                   BookServices bookServices)
        {
            database = connectionMultiplexer.GetDatabase();
            _mapper = mapper;
            _context = context;
            _cartServices = cartServices;
            _bookServices = bookServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCartByUserId(int id)
        {
            List<Cart> cartRedis = await database.GetRecordAsync<List<Cart>>(id.ToString());
            if (cartRedis is null)
            {
                cartRedis = await _context.Carts
                   .Where(c => c.UserId == id)
                   .Include(u => u.User)
                   .Include(b => b.Book)
                   .ToListAsync();
                await database.SetRecordAsync(id.ToString(), cartRedis, TimeSpan.FromDays(1));
            }
            return Ok(new { data = _mapper.Map<List<CartViewModel>>(cartRedis), success = true });
         }
        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartByUser(int id)
        {
            bool IsDeleteRedis = await database.KeyDeleteAsync(id.ToString());
            if (IsDeleteRedis)
            {
                await database.KeyDeleteAsync(id.ToString());
                return Ok(new { data = "Xoa thanh cong", success = true });
            }
            IsDeleteRedis = await _cartServices.DeleteCartAsync(id);
            if (IsDeleteRedis)
            {
                return Ok(new { data = "Xoa thanh cong", success = true });

            }
            return Ok(new { data = "Gio hang khong ton tai", success = true });    
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(CartPostModel NewItem)
        {
            //Kiem tra hop le
            if (!ModelState.IsValid)
            {
                return Ok(new { error_message = "Loi gio hang" });
            }

            //Kiem tra sach co ton tai
            Book book = await _bookServices.GetBookById(NewItem.BookId);
            if (book is null)
            {
                return Ok(new { error_message = "Sach khong ton tai" });
            }

            //Kiem tra so luong
            if (book.Quantity <= 0)
            {
                return Ok(new { error_message = "Sach da het so luong" });
            }

            //Kiem tra so luong ton
            if (book.Quantity - NewItem.Amount <= 0)
            {
                return Ok(new { error_message = "So luong sach con lai trong kho chi con " + NewItem.Amount });
            }


            Cart CurrentCart = await _cartServices.FindAsync(NewItem);
            if (CurrentCart is not null)
            {
                if (NewItem.Amount <= 0)
                {
                    List<Cart> carts = await _cartServices.DeleteCartById(CurrentCart);
                    return Ok(new { data = carts, success = true });

                }

                //Cap nhat lai gio hang
                CurrentCart.Amount += NewItem.Amount;
                CurrentCart.SubTotal = CurrentCart.Amount * book.Price;
                if (await _cartServices.UpdateAsync(CurrentCart))
                {
                    return Ok(new { data = await _cartServices.GetCartFromUser(NewItem.UserId), success = true });
                }
                else
                {
                    return Ok(new { error_message = "Co loi khi cap nhat gio hang" });
                }
            }
            else
            {
                if (NewItem.Amount <= 0)
                {
                    return Ok(new { error_message = "So luong sach khong hop le" });
                }

                Cart cart = new Cart
                {
                    UserId = NewItem.UserId,
                    BookId = NewItem.BookId,
                    Amount = NewItem.Amount,
                    SubTotal = NewItem.Amount * book.Price
                };
                if (await _cartServices.AddNewCartAsync(cart))
                {
                    return Ok(new { data = await _cartServices.GetCartFromUser(NewItem.UserId), success = true });
                }
                else
                {
                    return Ok(new { error_message = "Loi them gio hang" });
                }
            }
            //return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }
    }
}
