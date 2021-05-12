using System;
using System.Collections.Generic;
using BookStore.Models;

namespace BookStore.ViewModels.Invoice
{
    public class InvoicePostModel
    {
        public InvoicePostModel()
        {

        }
        public int? TotalMoney { get; set; }

        public int UserId { get; set; }

        public string Street_Address { get; set; }

        public int CityAddressId { get; set; }

        public int DistrictAddressId { get; set; }

        public int WardId { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

}
