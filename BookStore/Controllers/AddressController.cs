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
    public class AddressController : ControllerBase
    {
        private readonly bookstoreContext _context;

        public AddressController(bookstoreContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityAddress>>> GetCityAddresses()
        {
            return await _context.CityAddresses.Where(c=>c.Id == 1).Include(d=>d.DistrictAddresses).ToListAsync();
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityAddress>> GetCityAddress(int id)
        {
            var cityAddress = await _context.CityAddresses.FindAsync(id);

            if (cityAddress == null)
            {
                return NotFound();
            }

            return cityAddress;
        }

        // PUT: api/Address/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCityAddress(int id, CityAddress cityAddress)
        {
            if (id != cityAddress.Id)
            {
                return BadRequest();
            }

            _context.Entry(cityAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityAddressExists(id))
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

        // POST: api/Address
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityAddress>> PostCityAddress(CityAddress cityAddress)
        {
            _context.CityAddresses.Add(cityAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCityAddress", new { id = cityAddress.Id }, cityAddress);
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCityAddress(int id)
        {
            var cityAddress = await _context.CityAddresses.FindAsync(id);
            if (cityAddress == null)
            {
                return NotFound();
            }

            _context.CityAddresses.Remove(cityAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityAddressExists(int id)
        {
            return _context.CityAddresses.Any(e => e.Id == id);
        }
    }
}
