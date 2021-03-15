using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.BookComment;
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

        public async Task<IList<BookCommentViewModel>> GetCommentsInBook(int bookId)
        {
            var comments = await _bookstoreContext.BookComment
                .Include(c => c.Book)
                .Include(c => c.User)
                .Where(c => c.BookId == bookId)
                .ToListAsync();
            return _mapper.Map<IList<BookCommentViewModel>>(comments);
        }

        public async Task<BookCommentViewModel> AddNewComment(BookCommentDTO model)
        {
            if (! await _bookstoreContext.Book.AnyAsync(b => b.Id == model.BookId))
            {
                return new BookCommentViewModel { Comment = "Book không tồn tại." };
            }
            if (! await _bookstoreContext.Users.AnyAsync(u => u.Id == model.UserId))
            {
                return new BookCommentViewModel { Comment = "User không tồn tại." };
            }

            var book = _mapper.Map<BookComment>(model);

            await _bookstoreContext.BookComment.AddAsync(book);

            await _bookstoreContext.SaveChangesAsync();
            return _mapper.Map<BookCommentViewModel>(book);

        }
    }
}
