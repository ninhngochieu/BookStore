using System;
using BookStore.Models;

namespace BookStore.Services
{
    public class UserServices : Service
    {
        public UserServices(bookstoreContext bookstoreContext) : base(bookstoreContext)
        {
        }

        internal User getById(long userId)
        {
            return _context.Users.Find(userId);
        }
    }
}
