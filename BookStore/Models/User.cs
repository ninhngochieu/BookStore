using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class User
    {
        public User()
        {
            RoleId = 3;
        }
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
#nullable enable
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? RefreshToken { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public DateTime? TokenCreateAt { get; set; }

        //User - Role: 1 - 1
        [Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public IList<UserAddress> Addresses { get; set; }

    }
}