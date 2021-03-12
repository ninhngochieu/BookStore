using System.Collections.Generic;
using System.Linq;
using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Seed_Data
{
    public abstract class AppDbInitializer
    {
        
        internal static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<bookstoreContext>();

                //Add role
                if (!_context.Roles.Any())
                {
                    AddRoles(_context);
                    _context.SaveChanges();
                }
                if (!_context.Users.Any())
                {
                    AddUsers(_context);
                    _context.SaveChanges();
                }

            }
        }

        private static void AddUsers(bookstoreContext context)
        {
            IList<User> users = new List<User>();
            users.Add(new User { Username = "ninhngochieu", Password = "123456", RoleId = 1, Email="ninhngochieu@gmail.com",Name="Ninh Ngoc Hieu" });
            users.Add(new User { Username = "trinhduchieu", Password = "123456", RoleId = 2 });
            users.Add(new User { Username = "donguyenhoangson", Password = "123456", RoleId = 3 });
            users.Add(new User { Username = "duongbacdong", Password = "123456", RoleId = 4 });
            users.Add(new User { Username = "dinhduyphat", Password = "123456", RoleId = 4 });
            context.Users.AddRange(users);
        }

        private static void AddRoles(bookstoreContext context)
        {
            IList<Role> roles = new List<Role>();
            roles.Add(new Role { RoleName="Admin",Id=0});
            roles.Add(new Role { RoleName = "Manager" ,Id=1});
            roles.Add(new Role { RoleName = "User" ,Id=2});
            roles.Add(new Role { RoleName = "Store" ,Id=3});
            context.Roles.AddRange(roles);
        }
    }
}
