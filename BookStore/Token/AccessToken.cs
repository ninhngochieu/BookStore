using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookStore.Models;
using BookStore.Token;

namespace BookStore.TokenGenerators
{
    public class AccessToken
    {
        private TokenGenerator _tokenGenerator;
        private AuthenConfig _configuration;

        public AccessToken(TokenGenerator tokenGenerator, AuthenConfig configuration)
        {
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
        }

        internal string GenerateToken(User user)
        {
            List<Claim> _claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("id",user.Id.ToString()),
                new Claim("username",user.Username),
                new Claim("role", user.Roles.ToString())
            };
            return _tokenGenerator.GenerateToken(
                _configuration,
                _configuration.AccessTokenSecret,
                _claims
                );
        }
    }
}
