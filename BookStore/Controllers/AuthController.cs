using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Services;
using BookStore.Token;
using BookStore.TokenGenerators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserServices _userServices;

        private readonly AccessToken _accessToken;

        private readonly RefreshToken _refreshToken;

        public AuthController(UserServices userServices,AccessToken accessToken, RefreshToken refreshToken)
        {
            _userServices = userServices;
            _accessToken = accessToken;
            _refreshToken = refreshToken;
        }
        
        // POST: api/Auth
        [Route("login")]
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            bool isValidUser = !(user.Username is null) && !(user.Password is null);
            if (!isValidUser)
            {
                return BadRequest("Invalid User");
            }

            User fromDb = _userServices.Find(user);
            if(fromDb is null)
            {
                return Unauthorized("Failed to login");
            }

            //TokenPair tokenPair = new TokenPair()
            //{
            //    Access = _accessToken.GenerateToken(user),
            //    Refresh = _refreshToken.GenerateToken()
            //};
            //return Ok(tokenPair);
            return Ok(user);
        }
    }
}
