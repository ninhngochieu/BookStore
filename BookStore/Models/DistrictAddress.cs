﻿using System.Collections.Generic;

namespace BookStore.Models
{
    public class DistrictAddress
    {
        public DistrictAddress()
        {

        }
        public int Id { get; set; }
        public string DistrictName{ get; set; }
        public string Prefix { get; set; }
        //Navigate properties
        public int CityAddressId { get; set; }
        public virtual CityAddress CityAddress { get; set; }

        public virtual IList<Ward> Wards { get; set; }
    }
}