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
            IEnumerable<Book> book = _bookstoreContext.Book
                .Include(c => c.Category)
                .Include(c => c.Author)
                .Include(c=>c.Images);

            //if (model.AuthorName != null || model.AuthorName != "")
            //{
            //    book.Where(c => c.Author.AuthorName.Contains(model.AuthorName) || model.AuthorName == null || model.AuthorName.Trim() == "");
            //}
            //if (model.BookName != null || model.BookName != "")
            //{
            //    book.Where(c => c.BookName.Contains(model.BookName) || model.BookName == null || model.BookName.Trim() == "");
            //}
            //if (model.CategoryName != null || model.CategoryName != "")
            //{
            //    book.Where(c => c.Category.CategoryName.Contains(model.CategoryName) || model.CategoryName == null || model.CategoryName.Trim() == "");
            //}
            //if(model.SKU != null || model.SKU != "")
            //{
            //    book.Where(c => c.SKU.Contains(model.SKU) || model.SKU == null || model.SKU.Trim() == "");
            //}

            //Author
            if(model.AuthorName is not null)
            {
                book = book.Where(x => x.Author.AuthorName.ToLower().Contains(model.AuthorName.ToLower())).ToList();
            }
            //Book Name
            if (model.BookName is not null)
            {
                book = book.Where(x => x.BookName.ToLower().Contains(model.BookName.ToLower())).ToList();
            }            //Category
            if (model.CategoryName is not null)
            {
                book = book.Where(x => x.Category.CategoryName.ToLower().Contains(model.CategoryName.ToLower())).ToList();
            }            //Start price
            if (model.StartPrice is not null)
            {
                book = book.Where(x=>x.Price >=model.StartPrice).ToList();
            }            //End
            if (model.EndPrice is not null)
            {
                book = book.Where(x => x.Price <= model.EndPrice).ToList();
            }
            if (model.SKU is not null)
            {
                book = book.Where(x=>x.SKU==model.SKU).ToList();
            }
            //Sort
            if(model.SortByPriceAsc is not null)
            {
                book = book.OrderBy(b => b.Price).ToList();
            }
            if (model.SortByPriceDesc is not null)
            {
                book = book.OrderByDescending(b => b.Price).ToList();
            }
            if (model.SortByNameAsc is not null)
            {
                book = book.OrderBy(b => b.BookName).ToList();
            }
            if (model.SortByNameDesc is not null)
            {
                book = book.OrderByDescending(b => b.BookName).ToList();
            }
            var returnModel = _mapper.Map<IList<BookInfoViewModel>>(book.ToList());

            return returnModel;
        }
    }
}
