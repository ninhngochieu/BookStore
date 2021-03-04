using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.Book;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookServices : Service
    {
        private IMapper _mapper;

        public BookServices(bookstoreContext bookstoreContext, IMapper mapper) : base(bookstoreContext)
        {
            _mapper = mapper;
        }

        public async Task<IList<BookInfoViewModel>> GetAllBook()
        {
            var list = _mapper.Map<IList<BookInfoViewModel>>(await _bookstoreContext.Books.ToListAsync());
            return list;
        }

        
    }
}
