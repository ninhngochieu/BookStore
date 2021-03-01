using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    public partial class BookDetail
    {
        [Key]
        public long BookId { get; set; }
        public long? Quantity { get; set; }
        public long? DistributorId { get; set; }
        public long? PublicationDate { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public long? Pages { get; set; }
        [Column("SKU")]
        public string Sku { get; set; }
        public long? PublisherId { get; set; }
        public string Description { get; set; }
    }
}
