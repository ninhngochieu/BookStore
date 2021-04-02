using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.Author;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class AuthorServices : Service
    {
        public AuthorServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }

        internal Task<List<Author>> GetAllAuthor()
        {
            return _bookstoreContext.Author.ToListAsync();
        }

        internal async Task<object> GetAuthorById(int id)
        {
            var author = await _bookstoreContext.Author.FindAsync(id);

            if (author is null)
            {
                return "Khong tim thay tac gia";
            }
            return author;
        }

        internal async Task<bool> AddNewBook(Author author)
        {
            return await _bookstoreContext.SaveChangesAsync()!=0;
        }

        internal async Task<Author> FindAuthorAsync(int? id)
        {
            return await _bookstoreContext.Author.FindAsync(id??0);
        }

        internal async Task<bool> UpdateAuthorAsync(Author author, AuthorPostModel authorPostModel)
        {
            author.AuthorName = authorPostModel.AuthorName;
            return await _bookstoreContext.SaveChangesAsync()!=0;
        }

        internal async Task<bool> DeleteAuthorAsync(Author author)
        {
            _bookstoreContext.Author.Remove(author);
            return await _bookstoreContext.SaveChangesAsync()!=0;
        }
    }
}
