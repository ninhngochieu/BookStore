namespace BookStore.View_Models.Book
{
    public class SearchBookDTO
    {
        public string BookName { get; set; }
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
#nullable enable
        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; }
#nullable disable
    }
}
