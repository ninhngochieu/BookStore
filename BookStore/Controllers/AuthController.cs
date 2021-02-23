using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.ViewModels;
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
        [HttpPost, Route("login")]
        public ActionResult Post([FromBody] Login info)
        {
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
                    return Ok(new { Token = GenerateTokenString(user) });
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        private object GenerateTokenString(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"].ToString())),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return securityToken;
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
