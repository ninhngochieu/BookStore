using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookDetail
    {
        public int Quantity { get; set; }
        public string Distributor { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public int Pages { get; set; }
        public string SKU { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
    }
}
