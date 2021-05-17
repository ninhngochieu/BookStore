using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.ViewModels.Invoice;
using AutoMapper;
using BookStore.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly CartServices _cartServices;

        public InvoiceController(bookstoreContext context, IMapper mapper, CartServices cartServices)
        {
            _context = context;
            _mapper = mapper;
            _cartServices = cartServices;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.OrderByDescending(s=>s.CreateAt).ToListAsync();
        }

        // GET: api/Invoice/5
        [HttpGet]
        [Route("GetInvoiceByUserId/{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            //var invoice = await _context.Invoices.FindAsync(id);

            //if (invoice == null)
            //{
            //    return NotFound();
            //}

            //return invoice;
            List<Invoice> invoices = await _context.Invoices
                .Where(u => u.UserId == id)
                .Include(w => w.Ward)
                    .ThenInclude(d => d.DistrictAddress)
                    .ThenInclude(c => c.CityAddress)
                .OrderByDescending(c=>c.CreateAt)
                .ToListAsync();
            return Ok(new { data = invoices, success = true });
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByUserId(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(InvoicePostModel invoice)
        {
            Invoice NewInvoice = _mapper.Map<Invoice>(invoice);
            _context.Invoices.Add(NewInvoice);
            bool isSave = await _context.SaveChangesAsync() != 0;
            if (isSave)
            {
                await _cartServices.DeleteCartByUserIdAsync(NewInvoice.UserId);
                return Ok(new { data = new { InvoiceId = NewInvoice.Id }, success = true });
            }
            else
            {
                return Ok(new { error_message = "Co loi xay ra" });
            }
        }

        [HttpPost]
        [Route("CancelInvoice/{id}")]
        public async Task<ActionResult<Invoice>> CancelInvoice(int id)
        {
            Invoice invoice = await _context.Invoices.FindAsync(id);
            if(invoice is null)
            {
                return Ok(new { error_message = "Don hang nay khong ton tai" });
            }
            invoice.StatusId = 3;
            _ = await _context.SaveChangesAsync();
            List<Invoice> invoices = await _context.Invoices
                .Where(u => u.UserId == invoice.UserId)
                .Include(w => w.Ward)
                .ThenInclude(d => d.DistrictAddress)
                .ThenInclude(c => c.CityAddress)
                .OrderByDescending(s=>s.CreateAt)
                .ToListAsync();
            return Ok(new { data = invoices, success = true });

        }

        [HttpPost]
        [Route("CancelInvoiceAdmin/{id}")]
        public async Task<ActionResult<Invoice>> CancelInvoiceAdmin(int id)
        {
            Invoice invoice = await _context.Invoices.FindAsync(id);
            if (invoice is null)
            {
                return Ok(new { error_message = "Don hang nay khong ton tai" });
            }
            invoice.StatusId = 3;
            _ = await _context.SaveChangesAsync();
            List<Invoice> invoices = await _context.Invoices
                .Include(w => w.Ward)
                .ThenInclude(d => d.DistrictAddress)
                .ThenInclude(c => c.CityAddress)
                .OrderByDescending(s => s.CreateAt)
                .ToListAsync();
            return Ok(invoices);
        }

        [HttpPost]
        [Route("AcceptInvoiceAdmin/{id}")]
        public async Task<ActionResult<Invoice>> AcceptInvoiceAdmin(int id)
        {
            Invoice invoice = await _context.Invoices.FindAsync(id);
            if (invoice is null)
            {
                return Ok(new { error_message = "Don hang nay khong ton tai" });
            }
            invoice.StatusId = 2;
            _ = await _context.SaveChangesAsync();
            List<Invoice> invoices = await _context.Invoices
                .Include(w => w.Ward)
                .ThenInclude(d => d.DistrictAddress)
                .ThenInclude(c => c.CityAddress)
                .OrderByDescending(s => s.CreateAt)
                .ToListAsync();
            return Ok(invoices);
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }

    }
}
