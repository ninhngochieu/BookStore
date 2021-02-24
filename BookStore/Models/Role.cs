using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Index(nameof(Role1), IsUnique = true)]
    public partial class Role
    {
        [Key]
        public long Id { get; set; }
        [Column("Role")]
        public string Role1 { get; set; }
    }
}
