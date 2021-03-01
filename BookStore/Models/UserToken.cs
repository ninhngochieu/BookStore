using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Index(nameof(RefreshToken), IsUnique = true)]
    public partial class UserToken
    {
        [Key]
        public long UserId { get; set; }
        public string RefreshToken { get; set; }
        public long? CreateAt { get; set; }
    }
}
