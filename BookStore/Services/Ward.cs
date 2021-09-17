namespace BookStore.Models
{
    public class Ward
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int CityAddressId { get; set; }

        //Navigate Properties
        public int DistrictAddressId { get; set; }
        public virtual DistrictAddress DistrictAddress { get; set; }
    }
}