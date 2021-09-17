namespace BookStore.Models
{
    public class UserAddress
    {
        public UserAddress()
        {
            IsDefault = false;
        }
        public int Id { get; set; }
        public string? Street_Address { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
        //
        public int UserId { get; set; }
        public virtual User User { get; set; }
        //
        public int CityAddressId { get; set; }
        public virtual CityAddress CityAddress { get; set; }
        //
        public int DistrictAddressId { get; set; }
        public virtual DistrictAddress DistrictAddress { get; set; }

        public int WardId { get; set; }
        public virtual Ward Ward { get; set; }

        public bool IsDefault { get; set; }
    }
}