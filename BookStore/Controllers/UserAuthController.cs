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
using AutoMapper;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly AccessToken _accessToken;
        private readonly RefreshToken _refreshToken;
        private readonly IMapper _mapper;

        public UserAuthController(UserServices userServices,
            AccessToken accessToken,
            RefreshToken refreshToken,
            IMapper mapper)
        {
            _userServices = userServices;
            _accessToken = accessToken;
            _refreshToken = refreshToken;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid User");
            }

            User fromDb = await _userServices.DoLogin(user);
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

            if (user is null)
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
        [HttpPut("profile/{id}")]
        public async Task<IActionResult> Update(int id,[FromForm]UserInfoPostModel userVM)
        {
            if (!_userServices.isValidImage(userVM.Avatar))
            {
                return BadRequest("Invalid Image");
            }
            
            if (await _userServices.UpdateInfoAsync(userVM,id))
            {
                return Ok(new { data = userVM });
            }
            else
            {
                return BadRequest("Co loi xay ra");
            }

        }

        [HttpGet("profile/{id}")]
        public async Task<ActionResult<UserInfoViewModel>> GetUserInfo(int id)
        {
            User user = await _userServices.GetUserById(id);
            if (user is null)
            {
                return NotFound("Not found user");
            }
            UserInfoViewModel userInfo = _mapper.Map<User, UserInfoViewModel>(user);
            return Ok(new { data = userInfo, success = true });
        }
        
    }
}
