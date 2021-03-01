using System;
using BookStore.Models;

namespace BookStore.Services
{
    public abstract class Service
    {
        protected readonly bookstoreContext _context;

        public Service(bookstoreContext bookstoreContext)
        {
            _context = bookstoreContext;
        }

    }
}
