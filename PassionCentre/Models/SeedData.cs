using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PassionCentre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PassionCentre.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PassionCentreContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PassionCentreContext>>()))
            {
                // Look for any movies.
                if (context.Course.Any())
                {
                    return;   // DB has been seeded
                }

                context.Course.AddRange(
                    new Course
                    {
                        Title = "Switches and Routers",
                        Subject = "Networking",
                        Date = DateTime.Parse("2021-07-12"),
                        Username = "Tom Snr",
                        Description = "Networking beginner's guide"
                    },
                    new Course
                    {
                        Title = "Mobile App Development",
                        Subject = "Android",
                        Date = DateTime.Parse("2021-07-14"),
                        Username = "Daniel Ong",
                        Description = "Introduction to Android Application Development"
                    }
                );

                context.SaveChanges();
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                role.Description = "Admin User";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Staff").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Staff";
                role.Description = "Assist Staff in Trainer Role Management";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Trainer").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Trainer";
                role.Description = "Create & Conduct Courses";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "User";
                role.Description = "Normal User";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
