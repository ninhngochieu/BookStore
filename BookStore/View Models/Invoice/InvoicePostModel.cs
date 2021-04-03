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

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int ?TotalMoney { get; set; } = 0;
        public int UserId { get; set; }
        public int StatusId { get; set; } = 1;
    }
}
