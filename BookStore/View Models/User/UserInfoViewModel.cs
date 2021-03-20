using System.Collections.Generic;
using BookStore.Models;

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

        //public int? CityAddressId { get; set; }
        //public int? DistrictAddressId { get; set; }
        //public string? StreetAddress { get; set; }

        //public string? CityName { get; set; }
        //public string? DistrictName { get; set; }
        public List<UserAddress> Addresses { get; set; }

#nullable disable
    }
}
