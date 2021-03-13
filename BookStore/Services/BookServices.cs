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
        private readonly ImageServices _imageServices;

        public BookServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper, ImageServices imageServices) : base(bookstoreContext, webHostEnvironment, mapper)
        {
            _imageServices = imageServices;
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

        public IList<BookInfoViewModel> SearchBook(SearchBookDTO model)
        {
            IEnumerable<Book> book = _bookstoreContext.Book
                .Include(c => c.Category)
                .Include(c => c.Author)
                .Include(c => c.Images);

            //Author
            if (model.AuthorName is not null)
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
                book = book.Where(x => x.Price >= model.StartPrice).ToList();
            }            //End
            if (model.EndPrice is not null)
            {
                book = book.Where(x => x.Price <= model.EndPrice).ToList();
            }
           
            //Sort
            if (model.SortByPriceAsc is not null)
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
