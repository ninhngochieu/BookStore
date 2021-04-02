using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using System.Collections;
using BookStore.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly CityServices _cityServices;

        public AddressController(bookstoreContext context, CityServices cityServices)
        {
            _context = context;
            _cityServices = cityServices;
        }

        [HttpGet]
        [Route("GetCity")]
        public async Task<ActionResult<IEnumerable>> GetCityAddressAsync()
        {
            return Ok(new { data = await _cityServices.GetAllCityAndDistrictAsync(), success = true }); ;
        }

        [HttpGet]
        [Route("GetDistrict/{id}")]
        public async Task<ActionResult<IEnumerable>> GetDistrictAddressByCityIdAsync(int id)
        {
            return Ok(new { data = await _cityServices.GetDistrictByCityIdAsync(id), success = true });
        }
    }
}
