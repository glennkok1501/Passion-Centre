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
                        Title = "Networking",
                        Subject = "Networking",
                        Date = DateTime.Parse("2021-07-12"),
                        Username = "Tom Snr",
                        Description = "Networking beginner's guide"
                    },

                    new Course
                    {
                        //Title = "Ghostbusters ",
                        //ReleaseDate = DateTime.Parse("1984-3-13"),
                        //Genre = "Comedy",
                        //Price = 8.99M
                    },

                    new Course
                    {
                        //Title = "Ghostbusters 2",
                        //ReleaseDate = DateTime.Parse("1986-2-23"),
                        //Genre = "Comedy",
                        //Price = 9.99M
                    },

                    new Course
                    {
                        //Title = "Rio Bravo",
                        //ReleaseDate = DateTime.Parse("1959-4-15"),
                        //Genre = "Western",
                        //Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
