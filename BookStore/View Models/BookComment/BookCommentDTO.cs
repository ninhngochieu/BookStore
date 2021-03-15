namespace BookStore.View_Models.BookComment
{
    public class BookCommentDTO
    {
        public string Comment { get; set; }
        public double Rating { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
