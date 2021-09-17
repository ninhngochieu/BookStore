namespace BookStore.Models
{
    public class Cart
    {
        public Cart()
        {
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int SubTotal { get; set; } = 0;
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int Amount { get; set; } = 0;
    }
}
