using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PassionCentre.Models;

namespace PassionCentre.Data
{
    public class PassionCentreContext : DbContext
    {
        public PassionCentreContext (DbContextOptions<PassionCentreContext> options)
            : base(options)
        {
        }

        public DbSet<PassionCentre.Models.Course> Course { get; set; }
    }
}
