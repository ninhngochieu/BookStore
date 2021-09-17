namespace BookStore.Models
{
    public class BookComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        //
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        //
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
