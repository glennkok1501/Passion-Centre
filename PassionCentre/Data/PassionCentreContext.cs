using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PassionCentre.Models;

namespace PassionCentre.Data
{
    public class PassionCentreContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public PassionCentreContext (DbContextOptions<PassionCentreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<PassionCentre.Models.Course> Course { get; set; }

        public DbSet<PassionCentre.Models.AuditRecord> AuditRecords { get; set; }
    }
}
