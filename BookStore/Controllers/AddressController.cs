using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using System.Collections;
using BookStore.Services;
using BookStore.ViewModels.UserAddress;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly CityServices _cityServices;
        private readonly UserAddressService _userAddressService;
        private readonly UserServices _userServices;
        private readonly IMapper _mapper;

        public AddressController(bookstoreContext context,
                                 CityServices cityServices,
                                 UserAddressService userAddressService,
                                 UserServices userServices,
                                 IMapper mapper)
        {
            _context = context;
            _cityServices = cityServices;
            _userAddressService = userAddressService;
            _userServices = userServices;
            _mapper = mapper;
        }


        [HttpPost("{id}")]
        public async Task<ActionResult> SetDefaultAddressForUser(int id)
        {
            UserAddress userAddress = await _userAddressService.FindUserAddress(id);
            if (userAddress is null)
            {
                return Ok(new { error_message = "Khong tim thay dia chi" });
            }
            else
            {
                await _userAddressService.SetAllUserAddressToFalseAsync(userAddress.UserId);
                await _userAddressService.UpdateAddressStatusAsync(userAddress);
                return Ok(new { data = userAddress, success = true });
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddNewAddress(UserAddressPostModel userAddressPostModel)
        {
            if (!_userAddressService.IsValidAddress(userAddressPostModel))
            {
                return Ok(new { error_message = "Cu phap khong hop le" });
            }

            User user = await _userServices.GetUserById(userAddressPostModel.UserId);
            if (user is null)
            {
                return Ok(new { error_message = "Khong ton tai User" });
            }   

            var districtAddress = await _cityServices.GetCityAndDistrictAsync(userAddressPostModel);
            if (districtAddress is null)
            {
                return Ok(new { error_message = "Khong ton tai dia chi nay" });
            }

            await _userAddressService.SetAllUserAddressToFalseAsync(userAddressPostModel.UserId);
            UserAddress userAddress = _mapper.Map<UserAddress>(userAddressPostModel);
            bool isSaveUserAddress = await _userAddressService.AddUserAddress(userAddress);
            if (isSaveUserAddress)
            {
                return Ok(new { data = userAddress });
            }
            else
            {
                return Ok(new { error_message = "Co loi khi them dia chi moi" });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            UserAddress userAddress = await _userAddressService.FindUserAddress(id);
            if(userAddress is null)
            {
                return Ok(new { error_message= "Khong ton tai dia chi nay" });
            }
            bool IsDeleteAddress = await _userAddressService.DeleteAddress(userAddress);
            if (IsDeleteAddress)
            {
                return Ok(new {data ="Xoa dia chi thanh cong",success = true});
            }
            else
            {
                return Ok(new {error_message = "Co loi xay ra khi xoa" });
            }
        }
    }
}
