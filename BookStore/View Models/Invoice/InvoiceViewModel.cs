using System;
using System.Collections.Generic;
using BookStore.Models;

namespace BookStore.ViewModels.Invoice
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public int TotalMoney { get; set; }

        public int UserId { get; set; }
        public Models.User User { get; set; }

        public IList<InvoiceDetail> InvoiceDetails { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
