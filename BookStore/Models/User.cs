using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class User
    {
        public int? Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreateAt { get; set; }
        //User - Role: 1 - 1
        [Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

    }
}