namespace BookStore.Models
{
    public class Cart
    {
        public Cart()
        {
            Amount = 0;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }
    }
}
