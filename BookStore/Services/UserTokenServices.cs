using System;
using System.Linq;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class UserTokenServices : Service
    {
        public UserTokenServices(bookstoreContext bookstoreContext) : base(bookstoreContext)
        {
        }

        internal void createUserToken(User user, string refreshToken)
        {
            UserToken userToken = _context.UserTokens.Find(user.Id);
            if (userToken is null) // Chua co token
            {
                UserToken newUserToken = new UserToken
                {
                    UserId = user.Id??0,
                    RefreshToken = refreshToken,
                    CreateAt = DateTimeOffset.Now.ToUnixTimeSeconds()

                };
                _context.UserTokens.Add(newUserToken);
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.CreateAt = DateTimeOffset.Now.ToUnixTimeSeconds();
                _context.Entry(userToken).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        internal UserToken GetByToken(string refresh)
        {
            return _context.UserTokens.FirstOrDefault(r=>r.RefreshToken == refresh);
        }
    }
}
