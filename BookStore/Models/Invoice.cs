using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class Invoice
    {
        public Invoice()
        {
        }
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int? TotalMoney { get; set; } = 0;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual IList<InvoiceDetail> InvoiceDetails { get; set; }

        public int StatusId { get; set; } = 1;
        public virtual Status Status { get; set; }

        public bool IsOnlinePayment { get; set; } = false;

        public string Street_Address { get; set; }

        public int CityAddressId { get; set; }
        public virtual CityAddress CityAddress { get; set; }

        public int DistrictAddressId { get; set; }
        public virtual DistrictAddress DistrictAddress{ get; set; }

        public int WardId { get; set; }
        public virtual Ward Ward { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
