using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private bookstoreContext _context;
        private IConfiguration _configuration;

        public AuthController(bookstoreContext bookstoreContext, IConfiguration configuration)
        {
            _context = bookstoreContext;
            _configuration = configuration;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Post([FromBody] Login info)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            User user = _context.Users.Where(u => u.Username==info.Username).FirstOrDefault();
            if (user is null)
            {
                return Unauthorized();
            }
            else
            {
                bool isValid = user.Password.Equals(info.Password);
                if (isValid)
                {
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(1.5),
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                    });

                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        // GET: api/Auth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/Auth
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
