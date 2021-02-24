using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public partial class User
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long? Roles { get; set; }

        [ForeignKey(nameof(Roles))]
        [InverseProperty(nameof(Role.Users))]
        public virtual Role RolesNavigation { get; set; }
    }
}
