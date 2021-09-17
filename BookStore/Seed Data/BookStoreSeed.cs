using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.SeedData
{
    public class BookStoreSeed
    {
        public BookStoreSeed()
        {
        }

        internal static async Task Seed(bookstoreContext bookstoreContext)
        {
            string defaultExtension = ".json";
            if (!bookstoreContext.Roles.Any())
            {
                List<Role> roles = JsonSerializer.Deserialize<List<Role>>(File.ReadAllText("role" + defaultExtension));
                bookstoreContext.Roles.AddRange(roles);
            }

            if (!bookstoreContext.Users.Any())
            {
                List<User> users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText("user" + defaultExtension));
                bookstoreContext.Users.AddRange(users);
            }

            if (!bookstoreContext.UserAddress.Any())
            {
                List<UserAddress> userAddresses = JsonSerializer.Deserialize<List<UserAddress>>(File.ReadAllText("address" + defaultExtension));
                bookstoreContext.UserAddress.AddRange(userAddresses);
            }

            if (!bookstoreContext.Categories.Any())
            {
                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(File.ReadAllText("category" + defaultExtension));
                bookstoreContext.Categories.AddRange(categories);
            }

            if (!bookstoreContext.Author.Any())
            {
                List<Author> authors = JsonSerializer.Deserialize<List<Author>>(File.ReadAllText("author" + defaultExtension));
                bookstoreContext.Author.AddRange(authors);
            }

            if (!bookstoreContext.CityAddresses.Any())
            {
                List<CityAddress> cityAddresses = JsonSerializer.Deserialize<List<CityAddress>>(File.ReadAllText("city" + defaultExtension));
                bookstoreContext.CityAddresses.AddRange(cityAddresses);
            }

            if (!bookstoreContext.DistrictAddresses.Any())
            {
                List<DistrictAddress> districtAddresses = JsonSerializer.Deserialize<List<DistrictAddress>>(File.ReadAllText("district" + defaultExtension));
                bookstoreContext.DistrictAddresses.AddRange(districtAddresses);
            }

            if (!bookstoreContext.Ward.Any())
            {
                List<Ward> wards = JsonSerializer.Deserialize<List<Ward>>(File.ReadAllText("ward" + defaultExtension));
                bookstoreContext.Ward.AddRange(wards);
            }

            if (!bookstoreContext.Book.Any())
            {
                List<Book> books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText("book" + defaultExtension));
                bookstoreContext.Book.AddRange(books);
            }

            if (!bookstoreContext.BookComment.Any())
            {
                List<BookComment> bookComments = JsonSerializer.Deserialize<List<BookComment>>(File.ReadAllText("comment" + defaultExtension));
                bookstoreContext.BookComment.AddRange(bookComments);
            }

            if (!bookstoreContext.Carts.Any())
            {
                List<Cart> carts = JsonSerializer.Deserialize<List<Cart>>(File.ReadAllText("cart" + defaultExtension));
                bookstoreContext.Carts.AddRange(carts);
            }

            if (!bookstoreContext.BookImage.Any())
            {
                List<BookImage> bookImages = JsonSerializer.Deserialize<List<BookImage>>(File.ReadAllText("image" + defaultExtension));
                bookstoreContext.BookImage.AddRange(bookImages);
            }

            if (!bookstoreContext.Invoices.Any())
            {
                List<Invoice> invoices = JsonSerializer.Deserialize<List<Invoice>>(File.ReadAllText("invoice" + defaultExtension));
                bookstoreContext.Invoices.AddRange(invoices);
            }

            if (!bookstoreContext.InvoiceDetails.Any())
            {
                List<InvoiceDetail> invoiceDetails = JsonSerializer.Deserialize<List<InvoiceDetail>>(File.ReadAllText("invoice_detail" + defaultExtension));
                bookstoreContext.InvoiceDetails.AddRange(invoiceDetails);
            }

            if (!bookstoreContext.Statuses.Any())
            {
                List<Status> statuses = JsonSerializer.Deserialize<List<Status>>(File.ReadAllText("status" + defaultExtension));
                bookstoreContext.Statuses.AddRange(statuses);
            }

            await bookstoreContext.SaveChangesAsync();

        }
    }
}
