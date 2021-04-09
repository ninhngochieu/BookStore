using System;
namespace BookStore.ViewModels.DistrictAddress
{
    public class DistrictAddressViewModel
    {
        public DistrictAddressViewModel()
        {
        }
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public string Prefix { get; set; }
        //Navigate properties
        public int CityAddressId { get; set; }
    }
}
