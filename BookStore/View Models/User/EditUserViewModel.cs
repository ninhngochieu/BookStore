using System;
namespace BookStore.ViewModels.User
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
        }
        public int Id { get; set; }
        public string? Password { get; set; }
        #nullable enable
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        #nullable disable
    }
}
