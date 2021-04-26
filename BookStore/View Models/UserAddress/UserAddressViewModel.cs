using System;
using BookStore.Models;
using BookStore.ViewModels.CityAddress;
using BookStore.ViewModels.DistrictAddress;

namespace BookStore.ViewModels.UserAddress
{
    public class UserAddressViewModel
    {
        public UserAddressViewModel()
        {
        }
        public int Id { get; set; }
        public string Street_Address { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public int CityAddressId { get; set; }
        //public CityAddressViewModel CityAddress { get; set; }
        public int DistrictAddressId { get; set; }
        //public DistrictAddressViewModel DistrictAddress { get; set; }

        public int WardId { get; set; }
        public Ward Ward { get; set; }
        public bool IsDefault { get; set; }
    }
}
