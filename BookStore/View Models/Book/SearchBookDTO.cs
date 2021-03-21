namespace BookStore.View_Models.Book
{
    public class SearchBookDTO
    {
#nullable enable
        public string ?BookName { get; set; }
        public string ?CategoryName { get; set; }
        public string ?AuthorName { get; set; }
        public int ?CategoryId { get; set; }
        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; }

        public string? SortByPriceAsc { get; set; }
        public string? SortByPriceDesc { get; set; }
        public string? SortByNameAsc{ get; set; }
        public string? SortByNameDesc { get; set; }
        public string? SortByTimeAsc { get; set; }
        public string? SortByTimeDesc { get; set; }
        public int? TotalPerPage { get; set; }
        public int? CurrentPage { get; set; }
#nullable disable
    }
}
