using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            //writeJsonFile();
            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                try
                {
                    bookstoreContext bookstoreContext = scope.ServiceProvider.GetRequiredService<bookstoreContext>();
                    await bookstoreContext.Database.MigrateAsync();
                    await BookStoreSeed.Seed(bookstoreContext);
                }
                catch
                {
                    throw;
                }
            }
            host.Run();
        }

        private static void SeedData()
        {
            
        }

        private static async void writeJsonFile()
        {
            string defaultExtension = ".json";

            using (var bookstoreContext = new bookstoreContext())
            {
                List<Role> roles = bookstoreContext.Roles.ToList();
                File.WriteAllText("role" + defaultExtension, JsonConvert.SerializeObject(roles));

            }


            using (var bookstoreContext = new bookstoreContext())
            {
                List<User> users = bookstoreContext.Users.ToList();
                File.WriteAllText("user" + defaultExtension, JsonConvert.SerializeObject(users, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }

            using (var bookstoreContext = new bookstoreContext())
            {
                List<UserAddress> userAddresses = bookstoreContext.UserAddress.ToList();
                File.WriteAllText("address" + defaultExtension, JsonConvert.SerializeObject(userAddresses, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<CityAddress> cityAddresses = bookstoreContext.CityAddresses.ToList();
                File.WriteAllText("city" + defaultExtension, JsonConvert.SerializeObject(cityAddresses, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }

            using (var bookstoreContext = new bookstoreContext())
            {
                List<DistrictAddress> districtAddresses = bookstoreContext.DistrictAddresses.ToList();
                File.WriteAllText("district" + defaultExtension, JsonConvert.SerializeObject(districtAddresses, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }

            using (var bookstoreContext = new bookstoreContext())
            {
                List<Ward> wards = bookstoreContext.Ward.ToList();
                File.WriteAllText("ward" + defaultExtension, JsonConvert.SerializeObject(wards, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<Author> authors = bookstoreContext.Author.ToList();
                File.WriteAllText("author" + defaultExtension, JsonConvert.SerializeObject(authors, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<Book> books = bookstoreContext.Book.ToList();
                File.WriteAllText("book" + defaultExtension, JsonConvert.SerializeObject(books, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<BookComment> bookComments = bookstoreContext.BookComment.ToList();
                File.WriteAllText("comment" + defaultExtension, JsonConvert.SerializeObject(bookComments, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<BookImage> bookImages = bookstoreContext.BookImage.ToList();
                File.WriteAllText("image" + defaultExtension, JsonConvert.SerializeObject(bookImages, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<Cart> carts = bookstoreContext.Carts.ToList();
                File.WriteAllText("cart" + defaultExtension, JsonConvert.SerializeObject(carts, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<Category> categories = bookstoreContext.Categories.ToList();
                File.WriteAllText("category" + defaultExtension, JsonConvert.SerializeObject(categories, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<Invoice> invoices = bookstoreContext.Invoices.ToList();
                File.WriteAllText("invoice" + defaultExtension, JsonConvert.SerializeObject(invoices, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }
            using (var bookstoreContext = new bookstoreContext())
            {
                List<InvoiceDetail> invoiceDetails = bookstoreContext.InvoiceDetails.ToList();
                File.WriteAllText("invoice_detail" + defaultExtension, JsonConvert.SerializeObject(invoiceDetails, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }

            using (var bookstoreContext = new bookstoreContext())
            {
                List<Status> statuses = bookstoreContext.Statuses.ToList();
                File.WriteAllText("status" + defaultExtension, JsonConvert.SerializeObject(statuses, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })); ;
            }


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
