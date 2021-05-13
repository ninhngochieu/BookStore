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
            return Ok(new { data = await redis.GetRecordAsync<String>("Key") });
        }

        [HttpPost]
        public async Task<ActionResult> SetCartRedisAsync([FromBody] string value)
        {
            await redis.SetRecordAsync("Key", value, TimeSpan.FromDays(100));
            return Ok(new { data = await redis.GetRecordAsync<String>("Key") });
        }

    }
}
