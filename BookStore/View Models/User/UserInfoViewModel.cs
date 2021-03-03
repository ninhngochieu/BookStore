using System;
namespace BookStore.ViewModels.User
{
    public class UserInfoViewModel
    {
        public UserInfoViewModel()
        {
        }
        public string? Name{ get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
    }
}
