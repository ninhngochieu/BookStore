using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.ViewModels.CartViewModel;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly CartServices _cartServices;

        public CartController(bookstoreContext context, CartServices cartServices)
        {
            _context = context;
            _cartServices = cartServices;
        }

        //// GET: api/Cart
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        //{
        //    return await _context.Carts.ToListAsync();
        //}

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IList>> GetCart(int id)
        {
            var cart = await  _context.Carts
                .Where(c => c.UserId == id)
                .Include(u=>u.User)
                .Include(b=>b.Book)
                .ToListAsync();

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCart(int id, Cart cart)
        //{
        //    if (id != cart.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(cart).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CartExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Cart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                CurrentCart.Amount = NewItem.Amount;
                if(await _cartServices.UpdateAsync(CurrentCart))
                {
                    return Ok(new { data = CurrentCart, success = true});
                }
                else
                {
                    return Ok(new { error_message = "Co loi khi cap nhat gio hang"});
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
            //var cart = await _context.Carts.FindAsync(id);
            //if (cart == null)
            //{
            //    return NotFound();
            //}

            //_context.Carts.Remove(cart);
            //await _context.SaveChangesAsync();

            //return NoContent();
            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
