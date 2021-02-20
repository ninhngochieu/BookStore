using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    public partial class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public long? Roles { get; set; }
    }
}
