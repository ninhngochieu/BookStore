using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.Book;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookServices : Service
    {
        public BookServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }

        public async Task<IList<BookInfoViewModel>> GetAllBook()
        {
            var list = _mapper.Map<IList<BookInfoViewModel>>(await _bookstoreContext.Book.ToListAsync());
            return list;
        }

        internal async Task<bool> AddNewBookAsync(Book addNewBook)
        {
            try
            {
                return await _bookstoreContext.SaveChangesAsync() != 0;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
