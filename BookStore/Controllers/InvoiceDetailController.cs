using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly bookstoreContext _context;

        public InvoiceDetailController(bookstoreContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDetail>>> GetInvoiceDetails()
        {
            return await _context.InvoiceDetails.ToListAsync();
        }

        // GET: api/InvoiceDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetail>> GetInvoiceDetail(int id)
        {
            var invoiceDetail = await _context.InvoiceDetails
                .Where(u => u.InvoiceId == id)
                .Include(b => b.Book)
                .Include(i => i.Invoice)
                .ThenInclude(w => w.Ward)
                .ThenInclude(d => d.DistrictAddress)
                .ThenInclude(c => c.CityAddress)
                .ToListAsync();
            return Ok(new { data = invoiceDetail, success = true});
        }

        // PUT: api/InvoiceDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceDetail(int id, InvoiceDetail invoiceDetail)
        {
            if (id != invoiceDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceDetailExists(id))
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

        // POST: api/InvoiceDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvoiceDetail>> PostInvoiceDetail(List<InvoiceDetail> invoiceDetail)
        {
            _context.InvoiceDetails.AddRange(invoiceDetail);
            await _context.SaveChangesAsync();
            return Ok(new { data = "Them chi tiet don hang thanh cong", success = true });
        }

        // DELETE: api/InvoiceDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceDetail(int id)
        {
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            _context.InvoiceDetails.Remove(invoiceDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceDetailExists(int id)
        {
            return _context.InvoiceDetails.Any(e => e.Id == id);
        }
    }
}
