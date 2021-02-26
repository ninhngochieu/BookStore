using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BookStore.Models;
using BookStore.Token;
using BookStore.TokenGenerators;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AccessToken _accessToken;
        private readonly RefreshToken _refreshToken;
        private bookstoreContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(bookstoreContext bookstoreContext, IConfiguration configuration, AccessToken accessTokenGenerate, RefreshToken refreshTokenGenerator)
        {
            _context = bookstoreContext;
            _configuration = configuration;
            _accessToken = accessTokenGenerate;
            _refreshToken = refreshTokenGenerator;
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
                    //return Ok(new
                    //{
                    //    //token = new JwtSecurityTokenHandler().WriteToken(token) ,
                    //    accessToken = _accessTokenGenerate.GenerateToken(user),
                    //    refreshToken = _refreshTokenGenerator.GenerateToken()
                    //}) ;
                    return Ok(new
                    {
                        data = new TokenPair
                        {
                            Access = _accessToken.GenerateToken(user),
                            Refresh = _refreshToken.GenerateToken()
                        },
                        success = true
                    });
                 }
                else
                {
                    return Unauthorized();
                }
            }
        }

        //2 Van de
        //+ 
        [HttpPost]
        [AllowAnonymous]
        [Route("Refresh")]
        public ActionResult Refresh([FromBody] TokenPair token)
        {
            if (token.Refresh is null)
            {
                return BadRequest();
            }
            else
            {
                bool isValidToken = _refreshToken.Validate(token.Refresh);
                if (isValidToken)
                {
                    return Ok(new
                    {
                        data = token
                    });
                }
                else
                {
                    return BadRequest("Invalid refresh token");
                }
            }
        }
    }
}
