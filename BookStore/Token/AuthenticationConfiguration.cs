using System;
namespace BookStore.Token
{
    public class AuthenConfig
    {
        public AuthenConfig()
        {
        }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string AccessTokenSecret { get; set; }
        public double AccessTokenExpirationMinutes { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double RefreshTokenExpirationMinutes { get; set; }
    }
}
