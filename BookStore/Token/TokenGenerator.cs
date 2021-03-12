using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Token
{
    public class TokenGenerator
    {
        internal string GenerateToken(AuthenConfig _configuration,string SecretKey, double tokenExpirationMinutes, List<Claim> claims = null)
        {
            SecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)); ;
            SigningCredentials credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256); ;

            SecurityToken token = new JwtSecurityToken(
                issuer: _configuration.ValidIssuer,
                audience: _configuration.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenExpirationMinutes),
                signingCredentials: credentials
            ); ; ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
