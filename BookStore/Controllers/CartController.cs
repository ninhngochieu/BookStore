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
        private readonly BookServices _bookServices;

        public CartController(bookstoreContext context,
                              CartServices cartServices,
                              IMapper mapper,
                              BookServices bookServices)
        {
            _context = context;
            _cartServices = cartServices;
            _mapper = mapper;
            _bookServices = bookServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCartByUserId(int id)
        {
            var cart = await _context.Carts
               .Where(c => c.UserId == id)
               .Include(u => u.User)
               .Include(b => b.Book)
               .ToListAsync();
            if (cart is null)
            {
                return NotFound(new { data = "Empty cart", success = true });
            }

            return Ok(new { data = _mapper.Map<List<CartViewModel>>(cart), success = true });
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

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartByUser(int id)
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
        [HttpDelete]
        [Route("DeleteById/{id}")]
        public async Task<IActionResult> DeleteCartByCartId(int id)
        {
            List<Cart> carts = await _cartServices.DeleteCartByCartId(id);
            return Ok(new { data = carts, success = true });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCart(CartPostIdModel cart)
        {
            Cart cart1 = await _context.Carts.FindAsync(cart.Id);
            if(cart1 is null)
            {
                return Ok(new { error_message = "Gio hang khong ton tai" });
            }
            if (cart.Amount <= 0)
            {
                return Ok(new {data = await _cartServices.DeleteCartByCartId(cart1.Id), success = true});
            }
            Book book = await _context.Book.FindAsync(cart1.BookId);
            cart1.Amount = cart.Amount;
            cart1.SubTotal = book.Price * cart1.Amount;
            _context.Entry(cart1).State = EntityState.Modified; ;
            _ = await _context.SaveChangesAsync();
            List<Cart> carts  = await _context.Carts
             .Where(u => u.UserId == cart1.UserId)
             .Include(u => u.User)
             .Include(b => b.Book)
             .ToListAsync();

            return Ok(new { data = carts, success = true });
        }
    }
}