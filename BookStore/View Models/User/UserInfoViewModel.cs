using System.Collections.Generic;
using BookStore.Models;
using BookStore.ViewModels.UserAddress;

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
        public List<UserAddressViewModel> Addresses { get; set; }
        public bool? IsAccess { get; set; }
        public int ?RoleId { get; set; }

#nullable disable
    }
}
