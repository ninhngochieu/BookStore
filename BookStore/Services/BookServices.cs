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

        public async Task<IList<BookInfoViewModel>> SearchBook(SearchBookDTO model)
        {
            var book = _bookstoreContext.Book
                .Include(c => c.Category)
                .Include(c => c.Author);

            if (model.AuthorName != null || model.AuthorName != "")
            {
                book.Where(c => c.Author.AuthorName.Contains(model.AuthorName) || model.AuthorName == null || model.AuthorName.Trim() == "");
            }
            if (model.BookName != null || model.BookName != "")
            {
                book.Where(c => c.BookName.Contains(model.BookName) || model.BookName == null || model.BookName.Trim() == "");
            }
            if (model.CategoryName != null || model.CategoryName != "")
            {
                book.Where(c => c.Category.CategoryName.Contains(model.CategoryName) || model.CategoryName == null || model.CategoryName.Trim() == "");
            }
            if(model.SKU != null || model.SKU != "")
            {
                book.Where(c => c.SKU.Contains(model.SKU) || model.SKU == null || model.SKU.Trim() == "");
            }
            

            await book.ToListAsync();

            var returnModel = _mapper.Map<IList<BookInfoViewModel>>(book);

            return returnModel;
        }
    }
}
