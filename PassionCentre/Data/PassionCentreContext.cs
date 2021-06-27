using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PassionCentre.Models;

namespace PassionCentre.Data
{
    public class PassionCentreContext : IdentityDbContext<ApplicationUser>
    {
        public PassionCentreContext (DbContextOptions<PassionCentreContext> options)
            : base(options)
        {
        }

        public DbSet<PassionCentre.Models.Course> Course { get; set; }
        public DbSet<PassionCentre.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
