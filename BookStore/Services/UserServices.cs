using System;
using System.Linq;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class UserServices : Service
    {
        public UserServices(bookstoreContext bookstoreContext) : base(bookstoreContext)
        {
        }

        internal User Find(User user)
        {
            return _bookstoreContext.Users
             
                .FirstOrDefault();
        }
    }
}
