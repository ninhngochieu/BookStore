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
        private List<Claim> _claims;

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
                    _claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Username),
                        new Claim(ClaimTypes.Role,user.Password),
                    };
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims: _claims,
                        expires: DateTime.Now.AddHours(1.5),
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = ((DateTimeOffset)token.ValidTo).ToUnixTimeSeconds(),
                    });

                }
                else
                {
                    return Unauthorized();
                }
            }
        }
    }
}
