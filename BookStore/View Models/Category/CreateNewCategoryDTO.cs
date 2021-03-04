using System.ComponentModel.DataAnnotations;

namespace BookStore.View_Models.Category
{
    public class CreateNewCategoryDTO
    { 
        [Required]
        public string CategoryName { get; set; }
    }
}
