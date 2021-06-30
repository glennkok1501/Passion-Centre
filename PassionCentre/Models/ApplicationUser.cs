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
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid name.")]
        public string FullName { get; set; }

        [Display(Name ="Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Date/Time Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }

    }
}
