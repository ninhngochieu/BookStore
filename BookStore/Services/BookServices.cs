using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.Book;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookServices : Service
    {
        private readonly ImageServices _imageServices;
        private readonly BookCommentServices _bookCommentServices;

        public BookServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper, ImageServices imageServices, BookCommentServices bookCommentServices) : base(bookstoreContext, webHostEnvironment, mapper)
        {
            _imageServices = imageServices;
            _bookCommentServices = bookCommentServices;
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
            }catch(Exception)
            {
                return false;
            }
        }

        public async Task<ActionResult> SearchBook(SearchBookDTO model)
        {
            IQueryable<Book> book = _bookstoreContext.Book
                .Include(c => c.Category)
                .Include(c => c.Author)
                .Include(b => b.BookImage);
            if (model.CategoryId is not null)
            {
                book = book.Where(b => b.CategoryId == model.CategoryId);
            }
            //Author
            if (model.AuthorName is not null)
            {
                book = book.Where(x => x.Author.AuthorName.ToLower().Contains(model.AuthorName.ToLower()));
            }
            //Book Name
            if (model.BookName is not null)
            {
                book = book.Where(x => x.BookName.ToLower().Contains(model.BookName.ToLower()));
            }            //Category
            if (model.CategoryName is not null)
            {
                book = book.Where(x => x.Category.CategoryName.ToLower().Contains(model.CategoryName.ToLower()));
            }            //Start price
            if (model.StartPrice is not null)
            {
                book = book.Where(x => x.Price >= model.StartPrice);
            }            //End
            if (model.EndPrice is not null)
            {
                book = book.Where(x => x.Price <= model.EndPrice);
            }
           
            //Sort
            if (model.SortByPriceAsc is not null)
            {
                book = book.OrderBy(b => b.Price);
            }
            if (model.SortByPriceDesc is not null)
            {
                book = book.OrderByDescending(b => b.Price);
            }
            if (model.SortByNameAsc is not null)
            {
                book = book.OrderBy(b => b.BookName);
            }
            if (model.SortByNameDesc is not null)
            {
                book = book.OrderByDescending(b => b.BookName);
            }
            if(model.SortByTimeAsc is not null)
            {
                book = book.OrderBy(b => b.PublicationDate);
            }
            if (model.SortByTimeDesc is not null)
            {
                book = book.OrderByDescending(b => b.PublicationDate);
            }
            return await book.AsNoTracking().Paginate(model.TotalPerPage??8, model.CurrentPage??0);
            //var returnModel = _mapper.Map<IList<BookInfoViewModel>>(book.ToList());

            //foreach (var item in returnModel)
            //{
            //    item.Comments = await _bookCommentServices.GetCommentsInBook(item.Id);
            //}

        }

        internal async Task<bool> UpdateBookAsync(Book book, UpdateBookPostModel bookVM)
        {
            if(bookVM.AuthorId is not null)
            {
                book.AuthorId = bookVM.AuthorId??0;
            }

            if (bookVM.BookName is not null)
            {
                book.BookName = bookVM.BookName;
            }
            if (bookVM.CategoryId is not null)
            {
                book.CategoryId = bookVM.CategoryId??0;
            }
            if (bookVM.Description is not null)
            {
                book.Description = bookVM.Description;
            }
            if (bookVM.Price is not null)
            {
                book.Price = bookVM.Price??0;
            }
            if (bookVM.Private is not null)
            {
                book.Private = bookVM.Private;
            }
            //Reup image
            BookImage bookImage = new BookImage();
            bookImage.BookId = book.Id;

            if(bookVM.MainImage is not null)
            {
                book.MainImage = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookVM.MainImage.FileName;
                _imageServices.UploadImage(bookVM.MainImage, book.MainImage);
            }

            if(bookVM.Image1 is not null)
            {
                bookImage.Image1 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookVM.Image1.FileName;
                _imageServices.UploadImage(bookVM.Image1, bookImage.Image1);
            }
            if (bookVM.Image2 is not null)
            {
                bookImage.Image2 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookVM.Image2.FileName;
                _imageServices.UploadImage(bookVM.Image2, bookImage.Image2);
            }
            if (bookVM.Image3 is not null)
            {
                bookImage.Image3 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookVM.Image3.FileName;
                _imageServices.UploadImage(bookVM.Image3, bookImage.Image3);
            }
            if (bookVM.Image4 is not null)
            {
                bookImage.Image4 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookVM.Image4.FileName;
                _imageServices.UploadImage(bookVM.Image4, bookImage.Image4);
            }

            _bookstoreContext.Entry(book).State = EntityState.Modified;

            try
            {
                return await _bookstoreContext.SaveChangesAsync()!=0;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            };
        }
    }
}
