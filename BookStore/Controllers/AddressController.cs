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

            User user = await _userServices.GetUserById(userAddressPostModel.UserId??0);
            if (user is null)
            {
                return Ok(new { error_message = "Khong ton tai User" });
            }   

            var districtAddress = await _cityServices.GetCityAndDistrictAsync(userAddressPostModel);
            if (districtAddress is null)
            {
                return Ok(new { error_message = "Khong ton tai dia chi nay" });
            }

            await _userAddressService.SetAllUserAddressToFalseAsync(userAddressPostModel.UserId??0);
            UserAddress userAddress = _mapper.Map<UserAddress>(userAddressPostModel);
            bool isSaveUserAddress = await _userAddressService.AddUserAddress(userAddress);
            if (isSaveUserAddress)
            {
                return Ok(new { data = "Them dia chi thanh cong", success = true });
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

        [HttpGet]
        [Route("GetAllCity")]
        public async Task<ActionResult> GetAllCity()
        {
            return Ok(new { data = await _context.CityAddresses.ToListAsync() , success = true});
        }
        [HttpGet]
        [Route("GetDistrictById/{id}")]
        public async Task<ActionResult> GetDistrictById(int id)
        {
            return Ok(new { data = await _context.DistrictAddresses.Where(c=>c.CityAddressId==id).ToListAsync(), success = true });
        }
        [HttpGet]
        [Route("GetWardByCityIdAndDistrict/{cityId}/{districtId}")]
        public async Task<ActionResult> GetDistrictById(int cityId, int districtId)
        {
            return Ok(new { data = await _context.Ward
                .Where(c=>c.CityAddressId==cityId)
                .Where(d=>d.DistrictAddressId==districtId)
                .ToListAsync(), success = true });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> GetDistrict(int id, UserAddressPostModel userAddressPostModel)
        {
            UserAddress userAddress = await _context.UserAddress.Where(u => u.UserId==id).FirstOrDefaultAsync();
            if(userAddress is null)
            {
                return Ok(new { error_message = "Khong ton tai dia chi cua user nay"});
            }
            if(userAddressPostModel.Name is not null)
            {
                userAddress.Name = userAddressPostModel.Name;
            }
            if(userAddressPostModel.Phone is not null)
            {
                userAddress.Phone = userAddressPostModel.Phone;
            }
            if(userAddressPostModel.Street_Address is not null)
            {
                userAddress.Street_Address = userAddressPostModel.Street_Address;
            }

            userAddress.CityAddressId = userAddressPostModel.CityAddressId;
            userAddress.DistrictAddressId = userAddressPostModel.DistrictAddressId;
            userAddress.WardId = userAddressPostModel.WardId;

            await (_ = _context.SaveChangesAsync());
            return Ok(new{data = "Cap nhat thanh cong", success = true});
        }
        [HttpGet]
        [Route("GetDistrictAndWardByCityId/{id}")]
        public async Task<ActionResult> GetDistrictAndWardByCityId(int id)
        {
            return Ok(new { data = await _context.Ward
                .Where(c=>c.CityAddressId==id)
                .FirstOrDefaultAsync(), success = true });
        }
        [HttpGet]
        [Route("GetUserAddressByUserId/{id}")]
        public async Task<ActionResult> GetUserAddressByUserId(int id)
        {
            return Ok(new { data = await _context.UserAddress
                .Where(u=>u.UserId==id)
                .Include(s=>s.Ward)
                .ThenInclude(s=>s.DistrictAddress)
                .ThenInclude(s=>s.CityAddress)
                .ToListAsync(), success = true });;
        }
    }
}
