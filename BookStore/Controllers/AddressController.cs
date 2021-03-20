using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using System.Collections;

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

        [HttpGet]
        [Route("GetCity")]
        public async Task<ActionResult<IEnumerable>> GetCityAddressAsync()
        {
            return Ok(new { data = await _context.CityAddresses.ToListAsync(), success = true});
        }

        [HttpGet]
        [Route("GetDistrict/{id}")]
        public async Task<ActionResult<IEnumerable>> GetDistrictAddressAsync(int id)
        {
            return Ok(new { data = await _context.DistrictAddresses
                .Where(d => d.CityAddressId == id).ToListAsync()
            , success = true});
        }
    }
}
