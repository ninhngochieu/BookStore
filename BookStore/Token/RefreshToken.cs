using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using BookStore.TokenGenerators;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Token
{
    public class RefreshToken
    {
        private AuthenConfig _authenConfig;
        private TokenGenerator _tokenGenerator;

        public RefreshToken(AuthenConfig authenticationConfiguration, TokenGenerator tokenGenerator)
        {
            _authenConfig = authenticationConfiguration;
            _tokenGenerator = tokenGenerator;
        }

        internal string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
        internal string GenerateRefreshToken(int size=32)
        {
            byte[] refToken = new byte[size];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(refToken);
                return Convert.ToBase64String(refToken);
            }
        }

        internal string GenerateToken()
        {
            return _tokenGenerator.GenerateToken(
                _authenConfig,
                _authenConfig.RefreshTokenSecret);
        }

        internal bool Validate(string refreshToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _authenConfig.ValidIssuer,
                    ValidAudience = _authenConfig.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenConfig.RefreshTokenSecret)),
                    RequireExpirationTime = false,
                }; ;
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
