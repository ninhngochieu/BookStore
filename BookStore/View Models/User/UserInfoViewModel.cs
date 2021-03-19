namespace BookStore.View_Models.User
{
    public class UserInfoViewModel
    {
        public UserInfoViewModel()
        {
        }
#nullable enable
        public string? Name{ get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
#nullable disable
    }
}
