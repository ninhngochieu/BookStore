using System;
namespace BookStore.Token
{
    public class TokenPair
    {
        public TokenPair()
        {
        }
#nullable enable
        public string? Access{ get; set; }
        public string? Refresh{ get; set; }
    }
}
