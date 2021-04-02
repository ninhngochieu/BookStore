using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Services;
using BookStore.ViewModels.Invoice;
using AutoMapper;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly InvoiceService _invoiceService;
        private readonly IMapper _mapper;
        private readonly CartServices _cartServices;
        private readonly InvoiceDetailsService _invoiceDetailsService;

        public InvoiceController(bookstoreContext context, InvoiceService invoiceService, IMapper mapper, CartServices cartServices, InvoiceDetailsService invoiceDetailsService)
        {
            _context = context;
            _invoiceService = invoiceService;
            _mapper = mapper;
            _cartServices = cartServices;
            _invoiceDetailsService = invoiceDetailsService;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
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
        public async Task<IActionResult> PostInvoice(InvoicePostModel model)
        {
            //_context.Invoices.Add(invoice);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);


            //Tạo ra 1 invoice cho user khi nhấn thanh toán
            var invoice = _mapper.Map<Invoice>(model);

            await _context.Invoices.AddAsync(invoice);
            bool isSave = await _invoiceService.AddNewInvoiceAsync(invoice);

            if(!isSave)
            {
                return NotFound();
            }

            //Lấy món hàng trong cart rồi thêm vào InvoiceDetail
            var cartItems = await _cartServices.GetCartFromUser(model.UserId);
            var invoiceDetails = _mapper.Map<IList<InvoiceDetail>>(cartItems);

            //Gán id Invoice vào InvoiceDetail
            foreach (var item in invoiceDetails)
            {
                item.InvoiceId = invoice.Id;
            }

            //Lưu vào db
            isSave = await _invoiceDetailsService.SaveToDatabase(invoiceDetails);

            if(!isSave)
            {
                return NotFound();
            }
            return Ok(invoiceDetails);
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

        [HttpGet("InvoiceStatus")]
        public async Task<bool> SetStatus(int invoiceId, int statusId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            invoice.StatusId = statusId;
            
            return await _context.SaveChangesAsync() != 0;
        }
    }
}
