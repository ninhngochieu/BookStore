using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.ViewModels.UserAddress;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class CityServices : Service
    {
        public CityServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }

        internal async Task<List<CityAddress>> GetAllCityAndDistrictAsync()
        {
            return await _bookstoreContext.CityAddresses.Include(d => d.DistrictAddresses).ToListAsync();
        }

        internal async Task<List<DistrictAddress>> GetDistrictByCityIdAsync(int id)
        {
            return await _bookstoreContext.DistrictAddresses
                .Where(d => d.CityAddressId == id)
                   .ToListAsync();
        }

        internal async Task<DistrictAddress> GetCityAndDistrictAsync(UserAddressPostModel userAddressPostModel)
        {
            return await _bookstoreContext.DistrictAddresses.Where(c => c.CityAddressId == userAddressPostModel.CityAddressId)
                .Where(d => d.Id == userAddressPostModel.DistrictAddressId)
                .FirstOrDefaultAsync();
        }
    }
}
