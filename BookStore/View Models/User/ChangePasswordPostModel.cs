namespace BookStore.View_Models.User
{
    public class ChangePasswordPostModel
    {
        public ChangePasswordPostModel()
        {
        }
        public string old_password { get; set; }
        public string new_password { get; set; }
    }
}
