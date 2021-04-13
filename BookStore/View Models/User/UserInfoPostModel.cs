using Microsoft.AspNetCore.Http;

namespace BookStore.View_Models.User
{
    public class UserInfoPostModel
    {
        public UserInfoPostModel()
        {
        }
#nullable enable
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone{ get; set; }
        public string? StreetAddress { get; set; }
        public int? CityAddressId { get; set; }
        public int? DistrictAddressId { get; set; }
        public IFormFile? Avatar { get; set; }
        public bool ?IsAccess { get; set; }
        public int ?RoleId { get; set; }

#nullable disable
    }
}
