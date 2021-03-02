using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.ViewModels.User;
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

        internal async Task<User> Update(User user)
        {
            _bookstoreContext.Entry(user).State = EntityState.Modified;
            await _bookstoreContext.SaveChangesAsync();
            return user;
        }

        internal async Task<object> UpdateAsync(EditUserViewModel userVM)
        {
            User user = await _bookstoreContext.Users.FindAsync(userVM.Id);
            if(user is null)
            {
                return new NotFoundResult();
            }

            //Password
            if (userVM.Password is not null)
            {
                user.Password = userVM.Password;
                user.Avatar = userVM.Avatar;
                user.Email = userVM.Email;
                user.Name = userVM.Name;
                _bookstoreContext.Entry(user).State = EntityState.Modified;
                await _bookstoreContext.SaveChangesAsync();
            }
            else
            {
                return new NotFoundResult();
            }
            return userVM;
        }
    }
}
