using System.ComponentModel.DataAnnotations;

namespace BookStore.View_Models.Category
{
    public class ChangeCategoryNameDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
