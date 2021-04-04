using System;
using System.Collections.Generic;
using BookStore.Models;
using BookStore.View_Models.BookComment;
using BookStore.ViewModels.BookImage;

namespace BookStore.View_Models.Book
{
    public class BookInfoViewModel
    {
        public int Id { get; set; }
        public string? BookName { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? Description { get; set; }
        public string? MainImage { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? Private { get; set; }
        //public Models.Category Category { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        //public Author Author { get; set; }
        public IList<BookCommentViewModel> Comments { get; set; }
        public BookImageViewModel BookImageViewModel { get; set; }

    }
}
