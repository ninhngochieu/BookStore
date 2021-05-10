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
    public class UserAddressService : Service
    {
        public UserAddressService(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }

        internal async Task<UserAddress> FindUserAddress(int id)
        {
            return await _bookstoreContext.UserAddress.Where(u=>u.UserId==id).FirstOrDefaultAsync();
        }

        internal async Task<IList<UserAddress>> GetAllUserAddress(int userId)
        {
            return await _bookstoreContext.UserAddress
                .Where(u=>u.UserId==userId)
                .ToListAsync();
        }

        internal async Task SetAllUserAddressToFalseAsync(int userId)
        {
            IList<UserAddress> userAddresses = await GetAllUserAddress(userId);
            foreach (UserAddress ua in userAddresses)
            {
                ua.IsDefault = false;
            }
            _bookstoreContext.UpdateRange(userAddresses);
            await _bookstoreContext.SaveChangesAsync();
        }

        internal async Task UpdateAddressStatusAsync(UserAddress userAddress)
        {
            userAddress.IsDefault = true;
            _bookstoreContext.Update(userAddress);
            await _bookstoreContext.SaveChangesAsync();
        }

        internal bool IsValidAddress(UserAddressPostModel userAddressPostModel)
        {
            return !(userAddressPostModel.CityAddressId <= 0
                     && userAddressPostModel.DistrictAddressId <= 0
                     && userAddressPostModel.UserId <= 0);
        }

        internal async Task<bool> AddUserAddress(UserAddress userAddress)
        {
            await _bookstoreContext.AddAsync(userAddress);
            return await _bookstoreContext.SaveChangesAsync() != 0;
        }

        internal async Task<bool> DeleteAddress(UserAddress userAddress)
        {
            _bookstoreContext.Remove(userAddress);
            return await _bookstoreContext.SaveChangesAsync() != 0;
        }
    }
}
