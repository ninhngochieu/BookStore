using AutoMapper;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class InvoiceDetailsService : Service
    {
        public InvoiceDetailsService(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }

        internal async Task<bool> SaveToDatabase(IList<InvoiceDetail> items)
        {
            await _bookstoreContext.InvoiceDetails.AddRangeAsync(items);
            return await _bookstoreContext.SaveChangesAsync() != 0;
        }
    }
}
