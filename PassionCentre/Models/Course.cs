using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassionCentre.Models
{
    public class Course
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid title.")]
        [StringLength(60, ErrorMessage = "Title length can't be more than 60.")]
        public string Title { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid subject.")]
        [StringLength(60, ErrorMessage = "Subject length can't be more than 60.")]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time {  get; set; }

        [RegularExpression("^[a-zA-Z._@+-]*$", ErrorMessage = "Please enter a valid Username.")]
        public string Username { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid description.")]
        [StringLength(400, ErrorMessage = "Description length can't be more than 400.")]
        public string Description { get; set; }

        [Display(Name = "Meeting Details")]
        [StringLength(400, ErrorMessage = "Meeting Details length can't be more than 400.")]
        public string MeetingDetails { get; set; }
    }
}
