using System;
using BookStore.Models;

namespace BookStore.Services
{
    public abstract class Service
    {
        protected bookstoreContext _bookstoreContext;

        public Service(bookstoreContext bookstoreContext)
        {
            _bookstoreContext = bookstoreContext;
        }
    }
}
