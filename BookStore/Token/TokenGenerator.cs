using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.TokenGenerators
{
    public class TokenGenerator
    {
        internal string GenerateToken(AuthenConfig _configuration,string SecretKey, List<Claim> claims = null)
        {
            SecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)); ;
            SigningCredentials credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256); ;

            SecurityToken token = new JwtSecurityToken(
                issuer: _configuration.ValidIssuer,
                audience: _configuration.ValidAudience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_configuration.AccessTokenExpirationMinutes),
                signingCredentials: credentials
            ); ; ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
