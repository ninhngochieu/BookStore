﻿namespace BookStore.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string? Street_Address { get; set; }

        //
        public int UserId { get; set; }
        public User User { get; set; }
        //
        public int CityAddressId { get; set; }
        public CityAddress CityAddress { get; set; }
        //
        public int DistrictAddressId { get; set; }
        public DistrictAddress DistrictAddress { get; set; }
    }
}