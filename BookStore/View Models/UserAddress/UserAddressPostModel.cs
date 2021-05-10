using System;
namespace BookStore.ViewModels.UserAddress
{
    public class UserAddressPostModel
    {
        public UserAddressPostModel()
        {
            IsDefault = true;
        }
        public string? Street_Address { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
        //
        public int? UserId { get; set; }
        //
        public int CityAddressId { get; set; }
        //
        public int DistrictAddressId { get; set; }
        public int WardId { get; set; }

        public bool? IsDefault { get; set; }
    }
}
