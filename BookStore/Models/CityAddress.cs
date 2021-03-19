using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class CityAddress
    {
        public CityAddress()
        {
        }
        public int Id { get; set; }
        public string CityName { get; set; }
        public string CityCode{ get; set; }

        public IList<DistrictAddress> DistrictAddresses { get; set; }
    }
}
