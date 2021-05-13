using AutoMapper;
using BookStore.Models;
using BookStore.Modules;
using BookStore.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private string[] listStr;
        private readonly IDatabase redis;
        private readonly IConnectionMultiplexer _conn;
        private readonly IMapper _mapper;
        public RedisController(IConnectionMultiplexer conn, IMapper mapper)
        {
            _mapper = mapper;
            _conn = conn;
            redis = _conn.GetDatabase();
        }

        [HttpGet]
        public async Task<IActionResult> GetCartRedis(int userId)
        {
            string recordKey = userId.ToString();
            var cart = await redis.GetRecordAsync<IList<Cart>>(recordKey);
            
            if (cart is null)
            {
                return NotFound(new { data = "Empty cart", success = true });
            }
            //string recordKey = "string_" + DateTime.Now.ToString("yyyyMMdd_hhmm");
            //listStr = new string[2];
            //listStr[0] = "Not";
            //listStr[1] = "Working";
            //var serializedStr = JsonSerializer.Serialize(listStr);
            //await redis.StringSetAsync(recordKey, serializedStr);

            await redis.SetRecordAsync(recordKey, listStr);

            //var redisResult = await redis.GetRecordAsync<string[]>(recordKey);
            //var deserializedStr = JsonSerializer.Deserialize<string[]>(redisResult);
            //return Ok(cart);
            return Ok(new { data = _mapper.Map<List<CartViewModel>>(cart), success = true });
        }


        [HttpPost]
        public void SetCartRedis([FromBody] string value)
        {
        }

    }
}
