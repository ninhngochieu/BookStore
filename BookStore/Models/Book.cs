﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public Book()
        {
            Private = false;
            PublicationDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime PublicationDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string MainImage{ get; set; }
        public bool? Private { get; set; }
        //Navigation Properties
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public virtual BookImage BookImage { get; set; }

        //
        public virtual IList<BookComment> Comments { get; set; }

        public virtual IList<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
