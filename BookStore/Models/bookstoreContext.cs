﻿using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    public partial class bookstoreContext : DbContext
    {
        public bookstoreContext()
        {
        }

        public bookstoreContext(DbContextOptions<bookstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories{ get; set; }
        public virtual DbSet<Book> Book{ get; set; }
        public virtual DbSet<BookImage> BookImage{ get; set; }
        public virtual DbSet<BookComment> BookComment{ get; set; }
        public virtual DbSet<Author> Author{ get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<CityAddress> CityAddresses{ get; set; }
        public virtual DbSet<DistrictAddress> DistrictAddresses { get; set; }
        public virtual DbSet<UserAddress> UserAddress{ get; set; }
        public virtual DbSet<Cart> Carts{ get; set; }
        public virtual DbSet<Ward> Ward { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlite("Data Source=bookstore.db");
                //optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
