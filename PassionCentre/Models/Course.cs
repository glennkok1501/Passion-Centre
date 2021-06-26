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

        public string Title { get; set; }

        public string Subject { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time {  get; set; }

        public string Username { get; set; }

        public string Description { get; set; }

        [Display(Name = "Meeting Details")]
        public string MeetingDetails { get; set; }
    }
}
