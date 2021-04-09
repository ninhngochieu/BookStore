using System.Collections.Generic;
using BookStore.ViewModels.Role;
using BookStore.ViewModels.UserAddress;

namespace BookStore.ViewModels.User
{
    public class UserAdminViewModel
    {
        public UserAdminViewModel()
        {
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }

        public int RoleId { get; set; }
        public RoleViewModel RoleViewModel { get; set; }

        public IList<UserAddressViewModel> Addresses { get; set; }

        public bool IsAccess { get; set; } = true;
    }
}
