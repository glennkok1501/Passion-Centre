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
                        Username = "ArthurTan",
                        Description = "Networking beginner's guide"
                    },
                    new Course
                    {
                        Title = "Mobile App Development",
                        Subject = "Android",
                        Date = DateTime.Parse("2021-07-14"),
                        Username = "BobbyLim",
                        Description = "Introduction to Android Application Development"
                    },
                    new Course
                    {
                        Title = "How to Not Be a Graphic Designer",
                        Subject = "Adobe",
                        Date = DateTime.Parse("2021-07-14"),
                        Username = "JaymenNg",
                        Description = "Introduction to Adobe Photoshop"
                    }
                );

                context.SaveChanges();
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // Seeding Roles
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

            if (userManager.FindByNameAsync("superuser").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "superuser"; 
                user.FullName = "Superuser";
                user.Email = "passioncentre2021@gmail.com";
                user.BirthDate = DateTime.Now; 
                user.EmailConfirmed = true; 
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            //To remove before deployment. Used for testing purposes
            // Seeding Users
            if (userManager.FindByNameAsync("admin1").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin1";
                user.FullName = "admin1";
                user.Email = "admin1@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("admin2").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin2";
                user.FullName = "admin2";
                user.Email = "admin2@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("staff1").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "staff1";
                user.FullName = "staff";
                user.Email = "staff1@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            if (userManager.FindByNameAsync("staff2").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "staff2";
                user.FullName = "staff";
                user.Email = "staff2@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            if (userManager.FindByNameAsync("ArthurTan").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "ArthurTan";
                user.FullName = "Arthur Tan";
                user.Email = "trainer1@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Trainer").Wait();
                }
            }

            if (userManager.FindByNameAsync("BobbyLim").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "BobbyLim";
                user.FullName = "Bobby Lim";
                user.Email = "trainer2@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Trainer").Wait();
                }
            }

            if (userManager.FindByNameAsync("JaymenNg").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "JaymenNg";
                user.FullName = "Jaymen Ng";
                user.Email = "trainer3@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Trainer").Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "user1";
                user.FullName = "user";
                user.Email = "user1@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByNameAsync("user2").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "user2";
                user.FullName = "user";
                user.Email = "user2@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }
    }
}
