using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.SeedData
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
                }
                _context.SaveChanges();
            }
        }

        private static void AddRoles(bookstoreContext context)
        {
            IList<Role> roles = new List<Role>();
            roles.Add(new Role { RoleName="Admin"});
            roles.Add(new Role { RoleName = "Manager" });
            roles.Add(new Role { RoleName = "User" });
            roles.Add(new Role { RoleName = "Store" });
            context.Roles.AddRange(roles);
        }
    }
}
