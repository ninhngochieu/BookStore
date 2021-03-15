using AutoMapper;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookCommentServices : Service
    {
        public BookCommentServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }

        public async Task<IList<BookComment>> GetCommentsInBook(int bookId)
        {
            var comments = await _bookstoreContext.BookComment
                .Include(c => c.Book)
                .Include(c => c.User)
                .Where(c => c.BookId == bookId)
                .ToListAsync();
            return comments;
        }
    }
}
