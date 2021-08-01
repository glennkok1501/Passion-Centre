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
                        Description = "Networking beginners guide",
                        MeetingDetails = "Google Meets Meeting ID: 123123"
                    },
                    new Course
                    {
                        Title = "Mobile App Development",
                        Subject = "Android",
                        Date = DateTime.Parse("2021-07-14"),
                        Username = "WesleyTeo",
                        Description = "Introduction to Android Application Development",
                        MeetingDetails = "Skype Meeting ID: 123123"
                    },
                    new Course
                    {
                        Title = "How to Be a Graphics Designer",
                        Subject = "Adobe",
                        Date = DateTime.Parse("2021-07-14"),
                        Username = "JaymenNg",
                        Description = "Introduction to Adobe Photoshop",
                        MeetingDetails = "Zoom Meeting ID: 123123"
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
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.FullName = "admin";
                user.Email = "admin@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("staff").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "staff";
                user.FullName = "staff";
                user.Email = "staff@gmail.com";
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
                user.Email = "arthurtan@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Trainer").Wait();
                }
            }

            if (userManager.FindByNameAsync("WesleyTeo").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "WesleyTeo";
                user.FullName = "Wesley Teo";
                user.Email = "wesleyteo@gmail.com";
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
                user.Email = "jaymenng@gmail.com";
                user.BirthDate = DateTime.Now;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Trainer").Wait();
                }
            }

            if (userManager.FindByNameAsync("user").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "user";
                user.FullName = "user";
                user.Email = "user@gmail.com";
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
