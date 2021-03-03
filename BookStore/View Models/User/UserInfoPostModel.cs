using System;
using Microsoft.AspNetCore.Http;

namespace BookStore.ViewModels.User
{
    public class UserInfoPostModel
    {
        public UserInfoPostModel()
        {
        }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
