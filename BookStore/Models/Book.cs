using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    public partial class Book
    {
        [Key]
        public long Id { get; set; }
        public string BookName { get; set; }
        public long? Price { get; set; }
        public double? Rating { get; set; }
        public long? CategoryId { get; set; }
    }
}
