using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BookStore.Models;
using BookStore.Services;
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
        private readonly UserTokenServices _userTokenServices;
        private readonly UserServices _userServices;
        private bookstoreContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(bookstoreContext bookstoreContext, IConfiguration configuration, AccessToken accessTokenGenerate, RefreshToken refreshTokenGenerator, UserTokenServices userTokenServices, UserServices userServices)
        {
            _context = bookstoreContext;
            _configuration = configuration;
            _accessToken = accessTokenGenerate;
            _refreshToken = refreshTokenGenerator;
            _userTokenServices = userTokenServices;
            _userServices = userServices;
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
                    TokenPair tokenPair = new TokenPair
                    {
                        Access = _accessToken.GenerateToken(user),
                        Refresh = _refreshToken.GenerateToken()
                    };

                    //Insert or Update to User Token
                    _userTokenServices.createUserToken(user, tokenPair.Refresh);

                    return Ok(new
                    {
                        data = tokenPair,
                        success = true,
                    }) ;
                 }
                else
                {
                    return Unauthorized();
                }
            }
        }

        //2 Van de
        //+ Can biet ai gui refresh token len
        //+ Can vo hieu hoa refresh token neu bi stolen
        [HttpPost]
        [AllowAnonymous]
        [Route("Refresh")]
        public ActionResult Refresh([FromBody] TokenPair token)
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
            UserToken userToken = _userTokenServices.GetByToken(token.Refresh);
            if(userToken is null)
            {
                return NotFound("Refresh token not found");
            }

            isValidToken = _refreshToken.Validate(userToken.RefreshToken);
            if (!isValidToken)
            {
                //Boi vi controller co [Authorize] annotation co the tu validate token
                return Unauthorized("Token has expired");
            }

            User user = _userServices.getById(userToken.UserId);
            if(user is null)
            {
                return NotFound("User not found");
            }

            TokenPair tokenPair = new TokenPair
            {
                Access = _accessToken.GenerateToken(user),
                Refresh = _refreshToken.GenerateToken()
            };

            _userTokenServices.createUserToken(user, tokenPair.Refresh);

            return Ok(new
            {
                data = tokenPair,
                success = true
            });
        }
    }
}
