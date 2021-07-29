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
        [StringLength(60, ErrorMessage = "Name length can't be more than 60.")]
        public override string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid description.")]
        [StringLength(100, ErrorMessage = "Description length can't be more than 100.")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
    }
}
