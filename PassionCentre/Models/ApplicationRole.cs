using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PassionCentre.Models
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid Name.")]
        public override string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid description.")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
    }
}
