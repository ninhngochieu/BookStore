using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private bookstoreContext context;

        public LoginController(bookstoreContext context)
        {
            this.context = context;
        }
        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Login login)
        {
            User user = context.Users.Find(login.Username);
            if (user is null)
            {
                return Unauthorized("Failed to login");
            }
            else
            {
                bool isValidUser = login.Password.Equals(user.Password);
                if (isValidUser)
                {
                    return Ok(new { data = user });
                }
                else
                {
                    return Unauthorized("Failed to login");
                }
            }
        }
        // PUT: api/Login/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Login/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
