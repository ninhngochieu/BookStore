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
using BookStore.Modules.CustomAuthorization;

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
                return Ok(new { error_message = "Đăng nhập thất bại, vui lòng thử lại!" });
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
                return Unauthorized(new { error_message = "Refresh token invalid" });
            }

            isValidToken = _refreshToken.Validate(token.Refresh);
            if (!isValidToken)
            {
                return Unauthorized(new { error_message = "Refresh token invalid" });
            }

            //Validate from server
            User user = await _userServices.GetByRefreshToken(token.Refresh);

            if (user is null)
            {
                return Unauthorized(new { error_message = "Refresh token invalid" });
            }

            isValidToken = _refreshToken.Validate(user.RefreshToken);
            if (!isValidToken)
            {
                return Unauthorized(new { error_message = "Refresh token invalid" });
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
         [Authorize]
        public async Task<IActionResult> Update(int id,[FromForm]UserInfoPostModel userVM)
        {
            if (userVM.Avatar is not null)
            {
                if (!_userServices.isValidImage(userVM.Avatar)) return BadRequest(new { error_message = "Lỗi hình ảnh" });
            }

            UserInfoViewModel userInfoViewModel = await _userServices.UpdateInfoAsync(userVM, id);
            if(userInfoViewModel is not null)
            {
                return Ok(new { data = userInfoViewModel, success = true, message = "Cập nhật thông tin cá nhân thành công  "});
            }
            else
            {
                return BadRequest(new { error_message = "Có lỗi xảy ra" });
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
