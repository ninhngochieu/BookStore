namespace BookStore.Models
{
    public class BookImage
    {
        public int Id { get; set; }
#nullable enable
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
#nullable disable
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
