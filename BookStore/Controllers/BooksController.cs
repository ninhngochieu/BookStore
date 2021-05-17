﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Services;
using AutoMapper;
using BookStore.View_Models.Book;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookServices _bookServices;
        private readonly bookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly ImageServices _imageServices;

        public BooksController(BookServices bookServices,
            bookstoreContext bookstoreContext,
            IMapper mapper,
            ImageServices imageServices
            )
        {
            _bookServices = bookServices;
            _context = bookstoreContext;
            _mapper = mapper;
            _imageServices = imageServices;
        }
        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            var books = _bookServices.GetAllBook();
            return Ok(new { data = await books, success = true });
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookAsync(int id)
        {
            var book = _bookServices.FindBookAsync(id);

            if (book is null)
            {
                return Ok(new { error_message = "Khong tim thay san pham" });
            }

            return Ok(new { data = await book, success = true });
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Admin, Manager, Store")]
        public async Task<IActionResult> PutBook([FromForm]UpdateBookPostModel bookVM)
        {
            Book book = await _context.Book.FindAsync(bookVM.Id);
            if(book is null)
            {
                return Ok(new {error_message = "Khong tim thay sach" });
            }
            if (await _bookServices.UpdateBookAsync(book,bookVM))
            {
                return Ok(new { success = true, message = "Cap nhat thanh cong"});
            }
            else
            {
                return Ok(new { error_message = "Cap nhat that bai, co loi xay ra"});
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, Manager, Store")]
        public async Task<ActionResult<BookInfoViewModel>> PostBook([FromForm]CreateNewBookDTO bookDTO)
        {
            Book addNewBook = _mapper.Map<Book>(bookDTO);
            addNewBook.MainImage = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookDTO.MainImage.FileName;
            _context.Book.Add(addNewBook);
            bool isSave = await _bookServices.AddNewBookAsync(addNewBook);

            if (isSave)
            {
                //Save main image
                _imageServices.UploadImage(bookDTO.MainImage, addNewBook.MainImage);
                //Save many image
                BookImage bookImage = new BookImage();
                bookImage.BookId = addNewBook.Id;
                if (bookDTO.Image1 is not null)
                {
                    bookImage.Image1 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookDTO.Image1.FileName;
                    _imageServices.UploadImage(bookDTO.Image1, bookImage.Image1);
                }
                if (bookDTO.Image2 is not null)
                {
                    bookImage.Image2 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookDTO.Image2.FileName;
                    _imageServices.UploadImage(bookDTO.Image2, bookImage.Image2);
                }
                if (bookDTO.Image3 is not null)
                {
                    bookImage.Image3 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookDTO.Image3.FileName;
                    _imageServices.UploadImage(bookDTO.Image3, bookImage.Image3);
                }
                if (bookDTO.Image4 is not null)
                {
                    bookImage.Image4 = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + '_' + bookDTO.Image4.FileName;
                    _imageServices.UploadImage(bookDTO.Image4, bookImage.Image4);
                }
                _context.BookImage.Add(bookImage);
                await _context.SaveChangesAsync();
                return Ok(new { data = addNewBook, success = true });
            }
            else
            {
                return Ok(new { error_message = "Thêm sách thất bại, có lỗi xảy ra"});
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("SearchBook")]
        public async Task<ActionResult> SearchBook([FromBody] SearchBookDTO model)
        {
            return Ok(new {data = await _bookServices.SearchBook(model), success = true });
        }


        [HttpGet]
        [Route("TestPaging")]
        public async Task<ActionResult> Paging(int TotalPerPage = 8, int CurrentPage = 0)
        {
            return Ok(await _context.Book.AsNoTracking().Paginate(TotalPerPage, CurrentPage));
        }

    }
}
