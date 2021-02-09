using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public partial class Category
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
