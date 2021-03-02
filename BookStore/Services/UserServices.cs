using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class UserServices : Service
    {
        public UserServices(bookstoreContext bookstoreContext) : base(bookstoreContext)
        {
        }

        internal async Task<User> FindAsync(User user)
        {
            return await _bookstoreContext.Users
                .Where(u=>u.Username.Equals(user.Username))
                .Where(u=>u.Password.Equals(user.Password))
                .Include(r=>r.Role)
                .FirstOrDefaultAsync();
        }

        internal async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return await _bookstoreContext.Users.Include(r => r.Role).ToListAsync();
        }

        internal async Task createUserTokenAsync(User user, string refresh)
        {
            user.RefreshToken = refresh;
            user.TokenCreateAt = DateTime.Now;
            _bookstoreContext.Entry(user).State = EntityState.Modified;
            await _bookstoreContext.SaveChangesAsync();
        }

        internal async Task<User> GetByRefreshToken(string refresh)
        {
            return await _bookstoreContext.Users.Where(u=>u.RefreshToken.Equals(refresh)).FirstOrDefaultAsync();
        }
    }
}
