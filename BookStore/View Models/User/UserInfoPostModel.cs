using Microsoft.AspNetCore.Http;

namespace BookStore.View_Models.User
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
