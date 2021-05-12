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
        public int ?TotalMoney { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public IList<InvoiceDetail> InvoiceDetails { get; set; }

        public int StatusId { get; set; } = 1;
        public Status Status { get; set; }

        public bool IsOnlinePayment { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
