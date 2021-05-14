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
        private IDatabase database;
        private readonly IMapper _mapper;

        public CartRedisController(bookstoreContext context, IConnectionMultiplexer connectionMultiplexer, IMapper mapper, CartServices cartServices)
        {
            database = connectionMultiplexer.GetDatabase();
            _mapper = mapper;
            _context = context;
            _cartServices = cartServices;
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
            else
            {
                return Ok(new { data = "Gio hang khong ton tai", success = true });
            }
        }
    }
}
