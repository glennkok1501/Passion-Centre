using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PassionCentre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
