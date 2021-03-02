using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Services;
using BookStore.TokenGenerators;
using BookStore.Token;
using Microsoft.AspNetCore.Authorization;
using BookStore.ViewModels.User;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly UserServices _userServices;
        private AccessToken _accessToken;
        private readonly RefreshToken _refreshToken;

        public UserAuthController(UserServices userServices,
            AccessToken accessToken,
            RefreshToken refreshToken)
        {
            _userServices = userServices;
            _accessToken = accessToken;
            _refreshToken = refreshToken;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login(User user)
        {
            bool isValidUser = !(user.Username is null) && !(user.Password is null);
            if (!isValidUser)
            {
                return BadRequest("Invalid User");
            }

            User fromDb = await _userServices.FindAsync(user);
            if (fromDb is null)
            {
                return Unauthorized("Failed to login");
            }

            TokenPair tokenPair = new TokenPair()
            {
                Access = _accessToken.GenerateToken(fromDb),
                Refresh = _refreshToken.GenerateToken()
            };

            await _userServices.createUserTokenAsync(fromDb, tokenPair.Refresh);
            return Ok(new
            {
                data = tokenPair,
                success = true,
            });
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<User>> Refresh(TokenPair token)
        {
            bool isValidToken;
            //Validate from client
            if (token.Refresh is null)
            {
                return BadRequest("Refresh token is null");
            }

            isValidToken = _refreshToken.Validate(token.Refresh);
            if (!isValidToken)
            {
                return BadRequest("Refresh token has expired or invalid");
            }

            //Validate from server
            User user = await _userServices.GetByRefreshToken(token.Refresh);

            if(user is null)
            {
                return NotFound("Refresh token not found");
            }

            isValidToken = _refreshToken.Validate(user.RefreshToken);
            if (isValidToken)
            {
                return Unauthorized("Refresh token has expired");
            }

            //Create new token pair
            TokenPair tokenPair = new TokenPair
            {
                Access = _accessToken.GenerateToken(user),
                Refresh = _refreshToken.GenerateToken()
            };
            await _userServices.createUserTokenAsync(user, tokenPair.Refresh);
            return Ok(new
            {
                data = tokenPair,
                success = true,
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EditUserViewModel user)
        {
            bool isValidResponse = id == user.Id;
            if (!isValidResponse)
            {
                return BadRequest();
            }

            return Ok(_userServices.UpdateAsync(user));
        }
    }
}
