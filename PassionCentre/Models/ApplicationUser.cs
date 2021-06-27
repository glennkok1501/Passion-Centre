using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PassionCentre.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name ="Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

    }
}
